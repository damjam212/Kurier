using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace  webapi.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
  //  [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShippingController : ControllerBase
    {
        public const string SessionKeyName = "_Name";
        public const string SessionKeyAge = "_Age";
        private readonly MyDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ShippingController(MyDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        [EnableCors("AllowOrigin")]
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            // Sprawdzenie czy użytkownik jest uwierzytelniony
            if (User.Identity.IsAuthenticated)
            {
                return Ok("Autntykacja");
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if (!string.IsNullOrEmpty(userEmail))
                {
                    return Ok($"Adres email zalogowanego użytkownika: {userEmail}");
                }
            }

            return Ok("Użytkownik nie jest zalogowany lub nie ma adresu email.");
        }

        [HttpGet("showAllCookies")]
        public IActionResult ShowAllCookies()
        {
            var allCookies = Request.Cookies;
            return Ok("");
        }
    }
    // nie wolno wysylac obiektu z bazy danych 

    // Model danych wysyłanych w żądaniu POST
    public class ShippingData
    {
        public string Length { get; set; }
        public string Width { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string DestinationAddress { get; set; }
        public string SourceAddress { get; set; }
        public bool DeliverWeekend { get; set; }
        public string Priority { get; set; }
    }
}
