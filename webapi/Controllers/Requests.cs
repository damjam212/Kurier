using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Algorithm;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using webapi.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using static Google.Apis.Requests.BatchRequest;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private ApiManager apiManager;
        private readonly MyDbContext _context;
        private readonly IHttpClientFactory _clientFactory;

        
        

        public RequestsController(MyDbContext context, IHttpClientFactory clientFactory)
        {
             apiManager = new ApiManager();

            _context = context;
            _clientFactory = clientFactory;
        }
        [Authorize]
        [HttpGet("ws")]
        public async Task Get()
        {
            Console.WriteLine("polaczenie websocket");
            Console.WriteLine(HttpContext);
            Console.WriteLine(HttpContext.WebSockets);
            Console.WriteLine(HttpContext.WebSockets.IsWebSocketRequest);
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                Console.WriteLine("if polaczenie websocket");
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var cts = new CancellationTokenSource();
                string userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                Console.WriteLine("Api manager nawiązał połączenie WebSocket z klientem.\n");

                try
                {
                    // Odbieranie danych od klienta
                    while (webSocket.State == WebSocketState.Open)
                    {
                        var buffer = new byte[4096];

                        // Ustawienie timeout na 5 sekund
                        var timeoutTask = Task.Delay(5000);
                        var receiveTask = webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);

                        var completedTask = await Task.WhenAny(timeoutTask, receiveTask);

                        if (completedTask == timeoutTask)
                        {
                            // Przekroczono czas oczekiwania
                            Console.WriteLine("Przekroczono czas oczekiwania. Zamykanie połączenia.");
                            break;
                        }
                        var result = await receiveTask;

                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            string clientData = Encoding.UTF8.GetString(buffer, 0, result.Count);
                            dynamic formDataDynamic = JObject.Parse(clientData);
                            FormData formData = new FormData
                            {
                                Width = formDataDynamic.Width != null ? (int)formDataDynamic.Width : 0,
                                Height = formDataDynamic.Height != null ? (int)formDataDynamic.Height : 0,
                                Length = formDataDynamic.Length != null ? (int)formDataDynamic.Length : 0,
                                Currency = formDataDynamic.Currency != null ? formDataDynamic.Currency : "",
                                Weight = formDataDynamic.Weight != null ? (int)formDataDynamic.Weight : 0,
                                Source = formDataDynamic.Source != null ? formDataDynamic.Source : "",
                                Destination = formDataDynamic.Destination != null ? formDataDynamic.Destination : "",
                                DeliveryDate = formDataDynamic.DeliveryDate != null ? DateTime.Parse(formDataDynamic.DeliveryDate.ToString()) : DateTime.MinValue,
                                PickupDate = formDataDynamic.PickupDate != null ? DateTime.Parse(formDataDynamic.PickupDate.ToString()) : DateTime.MinValue,
                                DeliveryInWeekend = formDataDynamic.DeliveryInWeekend != null ? (bool)formDataDynamic.DeliveryInWeekend : false,
                                Prio = formDataDynamic.Prio != null ? formDataDynamic.Prio : ""
                            };

                            // Wyślij zapytania do wszystkich API
                            HttpClient httpClient = _clientFactory.CreateClient();
                            Console.WriteLine("Api manager wysyła zapytania\n");
                            List<OutputData> list = await apiManager.SendRequests(userEmail, _context, httpClient, formData,webSocket);

                            foreach (OutputData d in list)
                            {
                                Order order = new Order
                                {
                                    Email = userEmail,
                                    OrderId = d.InquiryId.ToString(),
                                    ApiType = d.apiType,
                                };
                                _context.Orders.Add(order);
                                _context.SaveChanges();

                                Console.WriteLine("Oto moje id\n");
                                Console.WriteLine(d.InquiryId.ToString());
                            }

                            Console.WriteLine("Api manager zakończył przetwarzanie danych od klienta.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas odbierania danych od klienta: {ex.Message}");
                }
                finally
                {
                    cts.Cancel(); 
                }
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }



        [Authorize]
        [HttpPost("pick_offer")]
        public async Task<ActionResult> PickOffer(OrderCreateDTO data)
        {

            HttpClient httpclient = _clientFactory.CreateClient();
                string inquiryId = data.InquiryId;
                var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == inquiryId);
                string userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

                if (order == null)
                {
                    return NotFound("Brak zamówienia o podanym ID.");
                }

            OrderPicked orderPicked  = await  apiManager.SendPickOfferRequest(userEmail, httpclient, inquiryId, order.ApiType);
            orderPicked.OrderKey = order.Id;
            _context.OrderPicked.Add(orderPicked);
            _context.SaveChanges();
            return Ok("udalo sie wybrac oferte");

        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            try
            {
                string userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                // Pobierz wszystkie zamówienia danego użytkownika z bazy danych
                var existingOrder = _context.Orders.FirstOrDefault(o => o.OrderId == id);

                if (existingOrder == null)
                {
                    // Zwróć błąd, jeśli zamówienie nie zostało znalezione
                    return NotFound($"Order with ID {id} not found.");
                }

                if (userEmail != existingOrder.Email)
                {
                    // Zwróć błąd, jeśli to nie jest zamówienie użytkownika
                    return BadRequest($"Not your order");
                }

                var httpClient = _clientFactory.CreateClient();
                Console.WriteLine("wysylam zapytanie");
                Console.WriteLine(existingOrder.GetEndpoint + "\\" + id);

                HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7076/api/Offerts" + "/" + id);
                Console.WriteLine("czekam na odpowiedz");
                // Sprawdź, czy zapytanie zostało pomyślnie przetworzone
                if (response.IsSuccessStatusCode)
                {
                    // Odczytaj obiekt Inquiry z treści odpowiedzi
                    Inquiry iq = await response.Content.ReadFromJsonAsync<Inquiry>();
                    return Ok(iq);
                }
                else
                {
                    // Zwróć błąd z odpowiedzi serwera
                    return BadRequest($"Error processing the request. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Obsługa innych błędów
                return BadRequest($"Error processing the request: {ex.Message}");
            }
        }
        [Authorize]
        [HttpGet("user/orders")]
        public async Task<ActionResult<List<Api1GetResponse>>> GetUserOrders()
        {
 
            try
            {
                Console.WriteLine("Zbieram dane na temat zamówień");
                string userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                var userOrders = _context.OrderPicked
                    .Include(op => op.Order)
                    .Where(op => op.Order.Email == userEmail)
                    .Select(op => new
                    {
                        OrderRequestId = op.OrderRequestId,
                        ApiType = op.Order.ApiType
                    })
                    .ToList();
                List<Api1GetResponse> orders= await apiManager.getOrdersData(_clientFactory, userOrders);
                // Zwróć zamówienia w odpowiedzi
                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Obsługa błędów
                return BadRequest($"Error processing the request: {ex.Message}");
            }
        }

    }
    class ApiError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public string Severity { get; set; }
        public string ErrorCode { get; set; }
        public Dictionary<string, string> FormattedMessagePlaceholderValues { get; set; }
    }
    public class Inquiry
    {
        public int Id { get; set; }

        public string InquiryId { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string DestinationAddress { get; set; }
        public string SourceAddress { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsCompany { get; set; }
        public bool DeliveryAtWeekend { get; set; }
        public string Priority { get; set; }
    }
    public class OfferDTO
    {
        public string inquiryId { get; set; }
        public string Price { get; set; }
        public string currency { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Expires { get; set; }
        public OfferStatus status { get; set; }
    }
    public enum OfferStatus
    {
        Pending,
        Accepted,
        Declined
    }

    public class Response
    {
        public int price { get; set; }
        public string inquiryId { get; set; }
        public DateTime validtillDate { get; set; }
        public string curr { get; set; }
        public string DeliveryAddress { get; set; }
        public string SourceAddress { get; set; }

    }

    public class OfferResponse
    {
        public Guid OfferRequestId { get; set; }
        public DateTime ValidTo { get; set; }
    }

    public class ValidationResponseError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public string Severity { get; set; }
        public string ErrorCode { get; set; }
        public object FormattedMessagePlaceholderValues { get; set; }
    }

    public class RequestFormData
    {
        public string index { get; set; }
        public string length { get; set; }
        public string width { get; set; }
        public string weight { get; set; }
        public string height { get; set; }
        public string DesAddress { get; set; }
        public string SourceAddress { get; set; }
        public bool DelAtWeekend { get; set; }
        public string Prio { get; set; }
        public int owner { get; set; }
        public int userId { get; set; }
    }
}
public class OrderCreateDTO
{
    public string InquiryId { get; set; }
}