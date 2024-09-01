﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateApi.Data;
using RealEstateApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext();
        private IConfiguration _config;

        public UsersController(IConfiguration config)
        {
            _config = config;
        }

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

        [HttpPost("[Action]")]
        public IActionResult Login([FromBody] User user)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Email==user.Email && u.Password==user.Password);

            if (currentUser is null) {
                return NotFound();
            }

            // generate JWT Token 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwt);
        }
    }

}
