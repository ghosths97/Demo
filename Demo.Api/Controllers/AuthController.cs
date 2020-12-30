using Demo.Exceptions;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]        
        [Route("/login")]
        public IActionResult Auth(Login login)
        {
            string token = String.Empty;
            if(login.Username == "test" && login.Password == "test")
            {
                token = GetToken(login);
            }
            else
            {
                throw new DemoException("Wrong Username password", System.Net.HttpStatusCode.Unauthorized);
            }

            return Ok(new { Username = login.Username, Token= token });

        }


        private string GetToken(Login login)
        {
            var user_name = "Ravi";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a_very_long_key_to_encrypt"));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] { new Claim("Sub", login.Username),
                                 new Claim("username", user_name),
                                 new Claim("role", "user")
            };

            JwtSecurityToken token = new JwtSecurityToken(
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials,
                claims: claims,
                issuer: "https://localhost:44360/",
                audience: "https://localhost:44360/"
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
