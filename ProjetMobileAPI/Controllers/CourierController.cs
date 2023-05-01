using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetMobileAPI.Data;
using ProjetMobileAPI.Models;


namespace ProjetMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly DataContext _context;

        public CourierController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Courier>>> GetCouriers()
        {
            return Ok(await _context.Couriers.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Courier>>> CreateUsers(Courier courier)
        {
            if (courier.FirstName == string.Empty || courier.LastName == string.Empty || courier.Adress == string.Empty || courier.PhoneNumber == null || courier.DateCourse == null)
                return BadRequest("Please enter valid information");

            _context.Couriers.Add(courier);
            await _context.SaveChangesAsync();

            return Ok(await _context.Couriers.ToListAsync());
        }
        
        [HttpPut]
        public async Task<ActionResult<List<Courier>>> updateUsers(Courier courier)
        {
            var dbCourier = await _context.Couriers.FindAsync(courier.Id);
            if (dbCourier == null)
                return BadRequest("Courier not found.");

            if (courier.FirstName == string.Empty) { courier.FirstName = dbCourier.FirstName; }
            if (courier.LastName == string.Empty) { courier.LastName = dbCourier.LastName; }
            if (courier.Adress == string.Empty) { courier.Adress = dbCourier.Adress; }
            if (courier.PhoneNumber == null) { courier.PhoneNumber = dbCourier.PhoneNumber; }
            if (courier.DateCourse == null) { courier.DateCourse = dbCourier.DateCourse; }

            await _context.SaveChangesAsync();

            return Ok(await _context.Couriers.ToListAsync());
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<List<Courier>>> DeleteUsers(int id)
        {
            var dbCourier = await _context.Couriers.FindAsync(id);
            if (dbCourier == null)
                return BadRequest("Courier not found.");

            _context.Couriers.Remove(dbCourier);
            await _context.SaveChangesAsync();

            return Ok(await _context.Couriers.ToListAsync());
        }
    }
}
