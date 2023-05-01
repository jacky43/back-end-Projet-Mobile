using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetMobileAPI.Data;
using ProjetMobileAPI.Models;

namespace ProjetMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetClient()
        {
            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Client>>> CreateUsers(Client client)
        {
            if (client.FirstName == string.Empty || client.LastName == string.Empty || client.Adress == string.Empty || client.PhoneNumber == null || client.DateCourse == null || client.Price == 0.0)
                return BadRequest("Please enter valid information");

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Client>>> updateClient(Client client)
        {
            var dbClient = await _context.Clients.FindAsync(client.Id);
            if (dbClient == null)
                return BadRequest("Client not found.");

            if (client.FirstName == string.Empty) { client.FirstName = dbClient.FirstName; }
            if (client.LastName == string.Empty) { client.LastName = dbClient.LastName; }
            if (client.Adress == string.Empty) { client.Adress = dbClient.Adress; }
            if (client.PhoneNumber == null) { client.PhoneNumber = dbClient.PhoneNumber; }
            if (client.DateCourse == null) { client.DateCourse = dbClient.DateCourse; }
            if(client.Price == 0.0) { client.Price = dbClient.Price; }

            await _context.SaveChangesAsync();

            return Ok(await _context.Couriers.ToListAsync());
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<List<Client>>> DeleteClient(int id)
        {
            var dbClient = await _context.Clients.FindAsync(id);
            if (dbClient == null)
                return BadRequest("Courier not found.");

            _context.Clients.Remove(dbClient);
            await _context.SaveChangesAsync();

            return Ok(await _context.Couriers.ToListAsync());
        }
    }
}
