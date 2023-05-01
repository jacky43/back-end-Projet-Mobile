using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetMobileAPI.Models;
using ProjetMobileAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjetMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> CreateUsers(User user)
        {
            if (user.FirstName == string.Empty || user.LastName == string.Empty || user.Email == string.Empty || user.BornDate == null || user.PassWord == null)
                return BadRequest("Please enter valid information");
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> updateUsers(User user)
        {
            var dbUser = await _context.Users.FindAsync(user.Id);
            if (dbUser == null)
                return BadRequest("Hero not found.");

            if(user.FirstName == string.Empty) { user.FirstName = dbUser.FirstName; }
            if (user.LastName == string.Empty) { user.LastName = dbUser.LastName; }
            if (user.Email == string.Empty) { user.Email = dbUser.Email; }
            if (user.BornDate == null) { user.BornDate = dbUser.BornDate; }
            if (user.PassWord == null) { user.PassWord = dbUser.PassWord; }

            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<List<User>>> DeleteUsers(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser == null)
                return BadRequest("Hero not found.");

            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }
    }
}
