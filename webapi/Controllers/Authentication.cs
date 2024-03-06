using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Google.Apis.Requests.BatchRequest;
using Microsoft.AspNetCore.Authentication.Google;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class Auth : ControllerBase
    {   //https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0
        //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0#cookies
        //public const string SessionKeyName = "_Name";
        //public const string SessionKeyAge = "_Age";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MyDbContext _context;
        public Auth(IHttpContextAccessor httpContextAccessor, MyDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;

        }
  
        [HttpGet("login-google")] // Zmiana endpointu dla logowania z Google
        public async Task<IActionResult> LoginWithGoogle()
        {
            var props = new AuthenticationProperties { RedirectUri = "Auth/google-callback" };
            return  Challenge(props, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (response.Principal == null) return BadRequest();

            var name = response.Principal.FindFirstValue(ClaimTypes.Name);
            var givenName = response.Principal.FindFirstValue(ClaimTypes.GivenName);
            var email = response.Principal.FindFirstValue(ClaimTypes.Email);
     
            return Redirect("https://localhost:3000");
        }
        [Authorize]
        [HttpGet("logout-google")]
        public async Task<IActionResult> LogoutFromGoogle()
        {
            await HttpContext.SignOutAsync();
            //  var callbackUrl = Url.Action("GoogleLogoutCallback", "Auth", values: null, protocol: Request.Scheme);
            //  var logoutUrl = $"https://accounts.google.com/o/oauth2/logout?client_id=484929956573-66ordm7uo4ug3i1oerd7p4992idsq2mh.apps.googleusercontent.com&redirect_uri={callbackUrl}";

            return Redirect("https://localhost:3000");
        }

        [HttpGet("google-logout-callback")]
        public IActionResult GoogleLogoutCallback()
        {
            // Tutaj możesz dodać logikę związana z wylogowaniem użytkownika po stronie Twojej aplikacji, jeśli jest taka potrzeba
            return Redirect("https://localhost:3000");
        }
        // Endpoint do wylogowania użytkownika
        [HttpGet("signout")]

        public async Task<IActionResult> SignOut()
        {
            //  await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //  HttpContext.
          //  HttpContext.User = null;
            //   await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //  await _httpContextAccessor.HttpContext.SignOutAsync();
             await HttpContext.SignOutAsync();
            //CookieAuthenticationDefaults.LogoutPath
           // return Ok();
            return Ok("Wylogowano pomyślnie!");
        }
        // Endpointy w tym kontrolerze będą wymagały autentykacji
        [HttpGet("get-user-email")]
        [Authorize]
        public IActionResult GetUserEmail()
        {
            // Sprawdzenie czy użytkownik jest uwierzytelniony
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

                if (!string.IsNullOrEmpty(userEmail))
                {
                    // Sprawdź, czy użytkownik o podanym emailu istnieje w bazie danych
                    var existingUser = _context.Users.FirstOrDefault(u => u.Email == userEmail);

                    if (existingUser == null)
                    {
                        // Utwórz nowego użytkownika i dodaj go do bazy danych
                        var newUser = new User { Email = userEmail };
                        _context.Users.Add(newUser);

                        // Dodaj rolę 'normal'
                        var userRole = new UserRole { UserId = newUser.Id, Email = newUser.Email, Role = "normal" };
                        _context.UserRoles.Add(userRole);

                        _context.SaveChanges();

                        var dataWithRole = new { message = "Autentykacja", email = userEmail, role = userRole.Role };
                        return Ok(dataWithRole);
                    }
                    else
                    {
                        // Pobierz wszystkie role użytkownika
                        var userRoles = _context.UserRoles.Where(ur => ur.UserId == existingUser.Id).Select(ur => ur.Role).ToList();

                        var data = new { message = "Autentykacja", email = userEmail, roles = userRoles };
                        return Ok(data);
                    }
                }
            }

            return Ok("Użytkownik nie jest zalogowany lub nie ma adresu email.");
        }


    }


}