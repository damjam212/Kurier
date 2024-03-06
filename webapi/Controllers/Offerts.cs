using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using webapi.DTO;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Offerts : ControllerBase
    {

        private readonly MyDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        public Offerts(MyDbContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<OffertDTO>>> GetOfferts()
        //{
        //    List<OffertDTO> output = new List<OffertDTO>();
        //    foreach(Request r in _context.Requests)
        //    {
        //        var httpClient = _clientFactory.CreateClient();
        //        HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7076/api/Offerts/ByInquiryId/"+r.InquiryId);

        //        OffertDTO responseObject = await response.Content.ReadFromJsonAsync<OffertDTO>();
        //        output.Add(responseObject);
        //    }

        //    return output;
        //}
    }
}
