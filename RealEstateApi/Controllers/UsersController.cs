using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Data;
using RealEstateApi.Models;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext();

        [HttpPost("[Action]")]
        public IActionResult Register([FromBody] User user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Email==user.Email);

            if (existingUser is not null)
            {
                return BadRequest("user with same email already exist");
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
