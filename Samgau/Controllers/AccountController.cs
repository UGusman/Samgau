using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Samgau.Models;
using Samgau.ViewModels;

namespace Samgau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private List<User> users = new List<User>
        {
            new User {Id=1, Login="scott@gmail.com", Password="12345" },
            new User {Id=2, Login="tiger@gmail.com", Password="12345" }
        };

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult GetToken([FromBody] LoginViewModel model)
        {
            var errorMessage = "Invalid e-mail address and/or password";
            if (!ModelState.IsValid)
                return BadRequest(errorMessage);
            var user = users.Where(x => x.Login == model.Email && x.Password == model.Password).FirstOrDefault();
            if (user == null)
                return BadRequest(errorMessage);
           
            var token =  GenerateToken(user);
            return Ok(token);
        }

        private TokenViewModel GenerateToken(User user)
        {
            var claims = new List<Claim>
      {
          new Claim(JwtRegisteredClaimNames.Sub, user.Login),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.Login)
      };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Authentication:JwtExpireDays"]));

            var token = new JwtSecurityToken(
              _configuration["Authentication:JwtIssuer"],
              _configuration["Authentication:JwtAudience"],
              claims,
              expires: expires,
              signingCredentials: creds
            );

            return new TokenViewModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                AccessTokenExpiration = expires,
                Login = user.Login
            };
        }
    }
}