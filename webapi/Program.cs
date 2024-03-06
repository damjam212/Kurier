using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration; // Dodaj tê przestrzeñ nazw, jeœli nie jest obecna
using Google;
using System.Data.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Google;
using System.Data;
using webapi;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.WebSockets;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMemoryCache(); 
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();



var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();


var CONF = builder.Configuration;

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//       // options.SessionStore=
//        options.Events = new CookieAuthenticationEvents
//        {
//            OnRedirectToLogin = context =>
//            {
//                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // Ustawienie statusu 401
//                return Task.CompletedTask;
//            },
//            // Inne zdarzenia...
//        };
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
//        options.AccessDeniedPath = new PathString("/Worker/");
//        options.LoginPath = new PathString("/auth/login");
//        //  options.AccessDeniedPath = "/Forbidden/";
//    });


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Schemat cookie dla autoryzacji
//    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // Schemat uwierzytelniania Google
//})
//.AddCookie() // Dodanie uwierzytelniania opartego o pliki cookie
//.AddGoogle(options =>
//{
//    options.ClientId = "484929956573-66ordm7uo4ug3i1oerd7p4992idsq2mh.apps.googleusercontent.com";
//    options.ClientSecret = "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";
//});


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Ustawienie statusu 401 (brak autoryzacji)
            return Task.CompletedTask;
        }
    };
})
           .AddGoogle( options =>
           {
               options.ClientId = "484929956573-66ordm7uo4ug3i1oerd7p4992idsq2mh.apps.googleusercontent.com";
               options.ClientSecret = "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";
               //   options.Events.OnRedirectToAuthorizationEndpoint
               options.Events.OnRedirectToAuthorizationEndpoint = context =>
               {
                   context.Response.Redirect(context.RedirectUri + "&prompt=consent");
                   return Task.CompletedTask;
               };
               options.Events.OnRemoteFailure = context =>
               {
                   context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Ustawienie statusu 401 (brak autoryzacji)
                   return Task.CompletedTask;
               };

           });
//builder.Services.AddAuthentication(o =>
//         {
//             o.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
//             o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//             o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//             //o.DefaultAuthenticateScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
//             //o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
//             //o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//             //  o.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//         })
//        .AddGoogle(options =>
//        {
//            options.ClientId = "484929956573-66ordm7uo4ug3i1oerd7p4992idsq2mh.apps.googleusercontent.com";
//            options.ClientSecret = "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";
//            // Tutaj mo¿esz okreœliæ inne opcje uwierzytelniania, np. CallbackPath, Scopes itp.
//            //options.CallbackPath = "/signin-google"; // Œcie¿ka wywo³ywana po autoryzacji
//            //options.SignedOutCallbackPath = "/signout-callback-google"; // Œcie¿ka wywo³ywana po wylogowaniu
//            //                                                            //options.RemoteSignOutPath = "/signout-google"; // Œcie¿ka do wylogowania na zdalnym serwerze
//            //                                                            //     options.CallbackPath = "/Auth/login-google"; // Œcie¿ka wywo³ywana po autoryzacji
//            //options.RemoteSignOutPath = "/Auth/signout"; // Œcie¿ka do wylogowania na zdalnym serwerze


//            //options.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
//            //options.TokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
//            //options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";

//            // Inne opcje konfiguracyjne

//            // Punkt koñcowy sesji
//          //  options.SignedOutRedirectUri = "https://localhost:3000/";
//        }).AddCookie(options =>
//    {
//        options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
//    });
//builder.Services.AddHttpContextAccessor();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = "Google";
//})
//.AddCookie().AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = "484929956573-66ordm7uo4ug3i1oerd7p4992idsq2mh.apps.googleusercontent.com";
//    googleOptions.ClientSecret = "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";
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
                   );
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//var websocketManager = app.ApplicationServices.GetService<WebSocketManager>();

app.UseWebSockets();
//app.Use(websocketManager.Acceptor);

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();
app.MapControllers();


app.Run();
