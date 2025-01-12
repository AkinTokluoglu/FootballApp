using FootballApp.Dtos;
using FootballApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FootballApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly FootballDbContext _context;

        public ProfileController(FootballDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UserUpdateDto updateDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Name = updateDto.Name ?? user.Name;
            user.Age = updateDto.Age ?? user.Age;
            user.Contact = updateDto.Contact ?? user.Contact;
            user.Position = updateDto.PositionsPlayed ?? user.Position;
            user.ProfilePictureUrl = updateDto.ProfilePicture ?? user.ProfilePictureUrl;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Profile updated successfully.");
        }
    }
}
