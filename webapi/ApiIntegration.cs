using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using webapi.DTO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Net.WebSockets;
using System.Text;

namespace webapi.Controllers
{
    public interface IApi
    {
        void Connect();
        Task<OutputData> SendRequest(string userEmail, HttpClient httpClient, FormData data);
    
    }

    // Implementacja pierwszego API (Nasze)
    public class Api1 : IApi
    {
        public string GetApiType()
        {
            return "api1";
        }
       
        public void Connect()
        {
            Console.WriteLine("Connecting to API 1");
        }

       public async Task<OutputData> SendRequest(string userEmail, HttpClient httpClient, FormData data)
        {
            string apiUrl = "https://localhost:7076/api/requests";

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");

            Console.WriteLine("wysylanie zapytania do api kurierskeigo");
            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"dobra odpowiedz");
               // Console.WriteLine(userEmail);
                OfferDTO offerDto = await response.Content.ReadFromJsonAsync<OfferDTO>();

                //_context.Orders.Add(order);
                //_context.SaveChanges();

                OutputData outData = new OutputData()
                {
                    InquiryId = Guid.Parse(offerDto.inquiryId),
                    Price = decimal.Parse(offerDto.Price),
                    Currency = offerDto.currency,
                    ExpiringAt = offerDto.Expires,
                    apiType = "api1",

                };
                // Możesz teraz użyć obiektu offerDto w swojej logice
                //   return offerDto;
                  return outData;
            }

            return null;

        }

        public async Task<Api1GetResponse> getOrder(HttpClient httpclient, string orderRequestId)
        {

            string Url2 = "https://localhost:7076/api/Offerts/" + orderRequestId;
            Console.WriteLine("wysylam zapytanie");

            HttpResponseMessage response = await httpclient.GetAsync(Url2);

            Console.WriteLine(response);

            try
            {
                if (response.IsSuccessStatusCode)
                {

                    Api1GetResponse respond = await response.Content.ReadFromJsonAsync<Api1GetResponse>();
                    return respond;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }

        public async Task<OrderPicked> SendPickOfferRequest(string userEmail, HttpClient httpClient, string inquiryId)
        {
            string Url2 = "https://localhost:7076/api/Offerts";
            Console.WriteLine("wysylam zapytanie");
            var data = new { inquiryId = inquiryId };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(Url2,content);

            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                OrderPicked op = new OrderPicked()
                {
                    OrderRequestId = inquiryId,
                    ValidTo = DateTime.Now,
                    OrderId = inquiryId
                };
                 await response.Content.ReadFromJsonAsync<Api1GetResponse>();
                return (op);
            }
            else
            {
                return null;
            }

        }
    }

    // Implementacja drugiego API (MINI)
    public class Api2 : IApi
    {
        public string GetApiType()
        {
            return "api2";
        }

        public async Task<string> GetTokenAsync(HttpClient httpClient)
        {
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://indentitymanager.snet.com.pl/connect/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials",
                    ["client_id"] = "team1b",
                    ["client_secret"] = "AAF66B2B-A4D2-413F-8F1B-3341FE7E45BC",
                    ["scope"] = "MiNI.Courier.API"
                })
            };

            var tokenResponse = await httpClient.SendAsync(tokenRequest);
            tokenResponse.EnsureSuccessStatusCode();

            var tokenResult = await tokenResponse.Content.ReadAsStringAsync();


            using (var doc = JsonDocument.Parse(tokenResult))
            {
                var accessToken = doc.RootElement.GetProperty("access_token").GetString();

                Console.WriteLine(accessToken);
                return accessToken;
            }
        }
        public void Connect()
        {
            Console.WriteLine("Connecting to API 2");
        }

        public async Task<OutputData> SendRequest(string userEmail, HttpClient httpClient, FormData data)
        {

            string Url = "https://mini.currier.api.snet.com.pl/Inquires"; // Zastąp to rzeczywistym adresem API

            //Utwórz klienta HttpClient
            var token = await GetTokenAsync(httpClient);
            MiniApiInquiry requestData = new MiniApiInquiry();
            requestData = requestData.CreateInquiryRequest(data);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));

            var sendRequestData = JsonContent.Create(requestData);
            string s = await sendRequestData.ReadAsStringAsync();

            Console.WriteLine(s);
            Console.WriteLine("wysylanie zapytania do api mini");

            HttpResponseMessage response2 = await httpClient.PostAsync(Url, sendRequestData);
            var rep = await response2.Content.ReadAsStringAsync();

            Console.WriteLine(rep);
            Console.WriteLine(response2);
            Console.WriteLine(response2.Content);

            if (response2.IsSuccessStatusCode)
            {

                string responseBody = await response2.Content.ReadAsStringAsync();
                Console.WriteLine($"Odpowiedź z serwera: {responseBody}");

                MiniApiInquiryResponse respond = await response2.Content.ReadFromJsonAsync<MiniApiInquiryResponse>();
                OutputData outData = new OutputData()
                {
                    InquiryId = respond.inquiryId,
                    Price = respond.totalPrice,
                    Currency = respond.currency,
                    ExpiringAt = respond.expiringAt,
                    apiType = "api2",
                };
                return (outData);
            }
            else
            {
                //var errors = JsonConvert.DeserializeObject<List<ApiError>>(rep);
                //// Przetwarzanie i wyświetlanie błędów
                //foreach (var error in errors)
                //{
                //    Console.WriteLine($"Property Name: {error.PropertyName}");
                //    Console.WriteLine($"Error Message: {error.ErrorMessage}");
                //    Console.WriteLine($"Severity: {error.Severity}");
                //    Console.WriteLine($"Error Code: {error.ErrorCode}");
                //    Console.WriteLine();
                //}
                return null;
            }
        }

        public async Task<OrderPicked> SendPickOfferRequest(string userEmail, HttpClient httpClient, string inquiryId)
        {

            OrderPicked order = null;
            InquiryApi2 inquiry = new InquiryApi2();
            Console.WriteLine("|" + Guid.Parse(inquiryId) + "|");
            inquiry.inquiryId = Guid.Parse(inquiryId);
            inquiry.name = "Name";
            inquiry.email = "mail@gmail.com";
            inquiry.address = new Address();

            string Url = "https://mini.currier.api.snet.com.pl/Offers"; // Zastąp to rzeczywistym adresem API
            var token = await GetTokenAsync(httpClient);

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            // httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));

            var content = JsonContent.Create(inquiry);
            Console.WriteLine("wysylam zapytanie");
            Console.WriteLine(content.ToString());
            Console.WriteLine(content);

            HttpResponseMessage response = await httpClient.PostAsync(Url, content);
            var rep = await response.Content.ReadAsStringAsync();
            Console.WriteLine(response.ToString());
            Console.WriteLine(response.Content);
            Console.WriteLine(rep.ToString());
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Zapytanie zostało wysłane pomyślnie.");

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Odpowiedź z serwera: {responseBody}");

                Api2PostOfferResponse respond = await response.Content.ReadFromJsonAsync<Api2PostOfferResponse>();

                //OfferResponse offerResponse = new OfferResponse()
                //{
                //    OfferRequestId = respond.inquiryId,
                //    ValidTo = respond.expiringAt
                //};

                //[Key]
                //public int Id { get; set; }
                //public string OrderRequestId { get; set; }
                //public string OrderId { get; set; }
                //public int OrderKey { get; set; }
                //public DateTime ValidTo { get; set; }

                //public Order Order { get; set; }

                 order = new OrderPicked
                {
                    OrderRequestId = respond.offerRequestId.ToString(),
                    ValidTo = respond.validTo,
                    OrderId = inquiryId
                };



                //return (offerResponse);
            }
            else
            {
                var errors = JsonConvert.DeserializeObject<List<ApiError>>(rep);
                // Przetwarzanie i wyświetlanie błędów
                if (errors != null)
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Property Name: {error.PropertyName}");
                        Console.WriteLine($"Error Message: {error.ErrorMessage}");
                        Console.WriteLine($"Severity: {error.Severity}");
                        Console.WriteLine($"Error Code: {error.ErrorCode}");
                        Console.WriteLine();
                    }
                Console.WriteLine($"Error: Requests.cs -> PickOffer -> switch(api2) -> invalid response");
            }
            return order;
        }

        public async Task<Api2GetStatusResponse> getOrder(HttpClient httpClient, string orderId)
        {

            Console.WriteLine("rozpoczynam szukanie zamówienia");
            Console.WriteLine(orderId);
           // string Url = "https://mini.currier.api.snet.com.pl/offer/" + orderId; // Zastąp to rzeczywistym adresem API
            string Url2 = "https://mini.currier.api.snet.com.pl/offer/request/" + orderId + "/status"; // Zastąp to rzeczywistym adresem API
            var token = await GetTokenAsync(httpClient);

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            // httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));

            Console.WriteLine("wysylam zapytanie");


            HttpResponseMessage response = await httpClient.GetAsync(Url2);
          //  HttpResponseMessage response2 = await httpClient.GetAsync(Url2);

            Console.WriteLine(response);
           // Console.WriteLine(response2);
            //var rep = await response.Content.ReadAsStringAsync();

            try
            {
                if (response.IsSuccessStatusCode )
                {

                    Api2GetStatusResponse respond = await response.Content.ReadFromJsonAsync<Api2GetStatusResponse>();
                    string Url = "https://mini.currier.api.snet.com.pl/offer/" + respond.offerId;
                    //HttpResponseMessage responseOffer = await httpClient.GetAsync(Url2);
                    //Console.WriteLine( "Ponizej wynik zapytania:" ); 
                    //Console.WriteLine( responseOffer );
                    //Console.WriteLine( responseOffer.IsSuccessStatusCode);
                    //if (responseOffer.IsSuccessStatusCode)
                    //{
                    //    Console.WriteLine("Zapytanie poprawne:");
                    //    Api2GetResponse respond2 = await responseOffer.Content.ReadFromJsonAsync<Api2GetResponse>();
                    //    return respond2;
                    //}
                        // Api2GetStatusResponse respond2 = await response2.Content.ReadFromJsonAsync<Api2GetStatusResponse>();
                        //   Console.WriteLine(respond2.isReady);
                        return respond;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                return null;
            }
            return null;
        }
    }
        public class ApiManager
        {

            Api1 api1;
            Api2 api2;
            private readonly List<IApi> apis = new List<IApi>();
            public ApiManager()
            {
                api1 = new Api1();
                api2 = new Api2();
                apis.Add(api1);
                apis.Add(api2);
            }
            public void AddApi(IApi api)
            {
                apis.Add(api);
        }

            async public Task<List<OutputData>> SendRequests(string userEmail, MyDbContext _context, HttpClient httpClient, FormData data, WebSocket webSocket)
            {
                List<OutputData> outData = new List<OutputData>();
                List<Task> tasks = new List<Task>();

                foreach (var api in apis)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        OutputData response = await api.SendRequest(userEmail, httpClient, data);
                        if (response != null)
                            outData.Add(response);
                        Console.WriteLine("Received response: " + response);
                        var json = System.Text.Json.JsonSerializer.Serialize(outData);
                        var buffer = Encoding.UTF8.GetBytes(json);

                        await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

                    }));
                }
                await Task.WhenAll(tasks);

                return outData;
            }

            async public Task<OrderPicked> SendPickOfferRequest(string userEmail, HttpClient httpClient, string inquiryId, string apiType)
            {
            OrderPicked orderPicked=null;
                switch (apiType)
                {
                    case "api1":
                    orderPicked = await api1.SendPickOfferRequest(userEmail, httpClient, inquiryId);

                    break;
                    case "api2":
                        Console.WriteLine("wybrano api2");
                         orderPicked = await api2.SendPickOfferRequest(userEmail, httpClient, inquiryId);
                        break;

                    case "api3":

                        break;
                }
                return orderPicked;
            }

            async public Task<List<Api1GetResponse>> getOrdersData(IHttpClientFactory _clientFactory, dynamic userOrders)
            {
                List<Api1GetResponse> finalorders= new List<Api1GetResponse>();
                foreach (var o in userOrders)
                {
                HttpClient httpclient = _clientFactory.CreateClient();
                Console.WriteLine("Wybrane id :"+o.OrderRequestId);
                    switch (o.ApiType)
                    {
                        case "api1":
                        // Nasze api
                        Api1GetResponse resp1 = await api1.getOrder(httpclient, o.OrderRequestId);
                        Console.WriteLine("wybrano api1");
                        finalorders.Add(resp1);
                        break;
                        case "api2":
                        Console.WriteLine("wybrano api2");
                        Api2GetStatusResponse resp = await api2.getOrder(httpclient, o.OrderRequestId);
                        Api1GetResponse resp2 = new Api1GetResponse()
                        {
                            orderId = string.Empty,
                            length = string.Empty,
                            width = string.Empty,
                            weight = string.Empty,
                            height = string.Empty,
                            destinationAddress = string.Empty,
                            sourceAddress = string.Empty,
                            pickupDate = DateTime.MinValue,
                            deliveryDate = DateTime.MinValue,
                            isCompany = false,
                            deliveryAtWeekend = false,
                            priority = string.Empty,
                            status = string.Empty,
                            isR=false
                        };
                        resp2.orderId = resp.offerId;
                        resp2.isR = resp.isReady;
                        Console.WriteLine(" api2 wykonalo swoją prace");
                            finalorders.Add(resp2);
                            // returnMessage = await api2.SendPickOfferRequest(userEmail, httpClient, inquiryId);
                            break;

                        case "api3":

                            break;
                    }
                }

                return finalorders;
            }
        }

    
        public class MiniApiInquiry
        {
            public Dimensions dimensions { get; set; }
            public string currency { get; set; }
            public double weight { get; set; }
            public string weightUnit { get; set; }
            public Address source { get; set; }
            public Address destination { get; set; }
            public DateTime pickupDate { get; set; }
            public DateTime deliveryDay { get; set; }
            public bool deliveryInWeekend { get; set; }
            public string priority { get; set; }
            public bool vipPackage { get; set; }
            public bool isComapny { get; set; }

            public MiniApiInquiry()
            {
                dimensions = new Dimensions();
                source = new Address("12", "10", "first", "Warsaw", "00-120", "Poland");
                destination = new Address("2", "4", "second", "Warsaw", "00-120", "Poland");
                pickupDate = DateTime.UtcNow.AddDays(2);
                deliveryDay = DateTime.UtcNow.AddDays(7);
                deliveryInWeekend = false;
                priority = "Medium";
                vipPackage = false;
                isComapny = false;
                currency = "Pln";
                weightUnit = "Kilograms";
                weight = 1;
            }

            public MiniApiInquiry CreateInquiryRequest(FormData d)
            {

                MiniApiInquiry requestData = new MiniApiInquiry();

                //requestData.priority = "Low";
                requestData.deliveryInWeekend = d.DeliveryInWeekend;
                requestData.isComapny = false;
                requestData.dimensions.length = d.Length;
                requestData.dimensions.width = d.Width;
                requestData.dimensions.height = d.Height;
                requestData.weight = d.Weight;
                //  requestData.deliveryDay = d.DeliveryDate;
                //   requestData.pickupDate = d.PickupDate;

                return requestData;
            }
        }

        public class MiniApiInquiryResponse
        {

            public Guid inquiryId { get; set; }
            public decimal totalPrice { get; set; }
            public string currency { get; set; }
            public DateTime expiringAt { get; set; }
            public List<PriceBreakDownItem> priceBreakDown { get; set; }

            public OutputData CreateInquiryResponse(MiniApiInquiryResponse data)
            {
                OutputData output = new OutputData()
                {
                    InquiryId = data.inquiryId,
                    Price = data.totalPrice,
                    Currency = data.currency,
                    ExpiringAt = data.expiringAt

                };
                return output;

            }

        }
        public class Dimensions
        {
            public int width { get; set; }
            public int height { get; set; }
            public int length { get; set; }
            public string dimensionUnit { get; set; }

            public Dimensions()
            {
                width = 1;
                height = 1;
                length = 1;
                dimensionUnit = "Meters";
            }
        }

        public class Address
        {
            public string houseNumber { get; set; }
            public string apartmentNumber { get; set; }
            public string street { get; set; }
            public string city { get; set; }
            public string zipCode { get; set; }
            public string country { get; set; }

            public Address()
            {
                houseNumber = "12";
                apartmentNumber = "12";
                street = "street";
                city = "Warsaw";
                zipCode = "00-120";
                country = "Poland";
            }

            public Address(string houseNumber, string apartmentNumber, string street, string city, string zipCode, string country)
            {
                this.houseNumber = houseNumber;
                this.apartmentNumber = apartmentNumber;
                this.street = street;
                this.city = city;
                this.zipCode = zipCode;
                this.country = country;
            }
        }
        public class PriceBreakDownItem
        {
            public decimal amount { get; set; }
            public string currency { get; set; }
            public string description { get; set; }
        }

        public class Api2PostOfferResponse
        {
        
         public Guid offerRequestId { get; set; }
        public DateTime validTo { get; set; }

    }

        public class OutputData
        {
            public Guid InquiryId { get; set; }
            public decimal Price { get; set; }
            public string Currency { get; set; }
            public DateTime ExpiringAt { get; set; }
            public string apiType { get; set; }

        }
        public class FormData
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public int Length { get; set; }
            public string Currency { get; set; }
            public int Weight { get; set; }
            public string Source { get; set; }
            public string Destination { get; set; }
            public DateTime DeliveryDate { get; set; }
            public DateTime PickupDate { get; set; }
            public bool DeliveryInWeekend { get; set; }
            public string Prio { get; set; }


        }

        public class InquiryApi2
        {
            public Guid inquiryId { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public Address address { get; set; }
        }

    public class Api2GetResponse
    {
        public int id { get; set; }
        public string inquiryId { get; set; }
        public string length { get; set; }
        public string width { get; set; }
        public string weight { get; set; }
        public string height { get; set; }
        public Address destinationAddress { get; set; }
        public Address sourceAddress { get; set; }
        public DateTime pickupDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public bool isCompany { get; set; }
        public bool deliveryAtWeekend { get; set; }
        public string priority { get; set; }

        // Nowo dodane pola
        public decimal totalPrice { get; set; }
        public string currency { get; set; }
        public DateTime inquireDate { get; set; }
        public DateTime offerRequestDate { get; set; }
        public DateTime decisionDate { get; set; }
        public string offerStatus { get; set; }
        public string buyerName { get; set; }
        public Address buyerAddress { get; set; }
    }

    public class Api2GetStatusResponse
        {
            public string offerId { get; set; }
            public bool isReady { get; set; }
            public DateTime timestamp { get; set; }
        }

    public class Api1GetResponse
    {
            public string orderId { get; set; }
            public string length { get; set; }
            public string width { get; set; }
            public string weight { get; set; }
            public string height { get; set; }
            public string destinationAddress { get; set; }
            public string sourceAddress { get; set; }
            public DateTime pickupDate { get; set; }
            public DateTime deliveryDate { get; set; }
            public bool isCompany { get; set; }
            public bool deliveryAtWeekend { get; set; }
            public string priority { get; set; }
            public string status { get; set; }
            public bool isR { get; set; }

    }

    }