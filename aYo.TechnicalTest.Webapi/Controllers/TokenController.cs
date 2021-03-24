using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using aYo.TechnicalTest.Data;
using aYo.TechnicalTest.Models;
using aYo.TechnicalTest.Services;
using aYo.TechnicalTest.Webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace aYo.TechnicalTest.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUserService _userService;

        public TokenController(IConfiguration config, IUserService userService)
        {
            _configuration = config;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplicationUserModel userData)
        {

            if (userData != null && userData.Email != null && userData.Password != null)
            {
                var user = await GetUser(userData.Email, userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("UserName", user.UserName),
                    new Claim("Email", user.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApplicationUserCreate _userData)
        {

            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);

                if (user == null)
                {
                    return Ok(_userService.CreateUser(new ApplicationUser
                    {
                        Email = _userData.Email,
                        CreatedDate = TimeSpan.FromTicks(DateTime.Now.Ticks),
                        FirstName = _userData.FirstName,
                        LastName = _userData.LastName,
                        Password = _userData.Password,
                        UserName = _userData.UserName
                    }));
                }
                else
                {
                    return BadRequest("User already exist");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<ApplicationUser> GetUser(string email, string password)
        {
            return await this._userService.GetUser(email, password);
        }
    }
}