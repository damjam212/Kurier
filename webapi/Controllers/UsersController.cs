using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektNetReact;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi;

// ... inne usingi i definicje

[ApiController]
[Route("api/users")] // Przykładowa ścieżka endpointu API dla użytkowników
public class UsersController : ControllerBase
{
    private readonly MyDbContext _context;

    public UsersController(MyDbContext context)
    {
        _context = context;
    }

    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    //{
    //    //// Pobierz wszystkich użytkowników z tabeli Users
    //    /////
    //    try
    //    {
    //        //User newItem = new User { Name = "Adam", Id = 21 };
    //        //_context.Users.Add(newItem);
    //        //await _context.SaveChangesAsync();

    //        //return Ok(newItem); // Zwróć nowo dodany element w odpowiedzi HTTP OK

    //        var users = await _context.Users.ToListAsync();
    //        return Ok(users);
    //        //  _context.Users.

    //       // return Ok(newItem);
    //    }
    //    catch (Exception ex)
    //    {
    //        return Ok(ex.Message);
    //    }

    //    //if (users == null || users.Count == 0)
    //    //{
    //    //    return NotFound(); // Zwróć NotFound, jeśli nie znaleziono żadnych użytkowników
    //    //}

    //    return Ok("udalo sie"); // Zwróć listę użytkowników w odpowiedzi HTTP OK
    //}

}
