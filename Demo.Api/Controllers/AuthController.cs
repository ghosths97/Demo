using Demo.Api.Models.Identity;
using Demo.Exceptions;
using Demo.Models;
using Demo.Shared.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpPost]        
        [Route("/login")]
        public async Task<IActionResult> Auth(LoginRequest login)
        {
            _logger.LogDebug("logging in:" + login.Email);

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                _logger.LogDebug("logging failed");
                throw new DemoException("Wrong Username password", System.Net.HttpStatusCode.Unauthorized);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            string token = String.Empty;
            if(result.Succeeded)
            {
                var userRoleNames = await _userManager.GetRolesAsync(user);
                var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name)).FirstOrDefault();
                var roleClaims = await _roleManager.GetClaimsAsync(userRoles);
                roleClaims.Add(new Claim(ClaimTypes.Email, user.Email.ToString()));
                roleClaims.Add(new Claim("Id", user.Id));

                token = GenerateJwtToken(user, roleClaims);
                _logger.LogDebug("logging success");
            }
            else
            {
                _logger.LogDebug("logging failed");
                throw new DemoException("Wrong Username password", System.Net.HttpStatusCode.Unauthorized);
            }

            return Ok(new LoginResponse() { Username = login.Email, Token = token });
        }
        
        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Email = registerRequest.Email,
                    UserName = registerRequest.Email,
                    Name = registerRequest.Name,
                    PhoneNumber = registerRequest.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, registerRequest.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }

            return Created("/login", new RegisterResponse() { Message = "User created successfully" });
        }


        private string GenerateJwtToken(User user, IList<Claim> claims)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("a_very_long_key_to_encrypt");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GetToken(LoginRequest login)
        {
            var user_name = "Ravi";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a_very_long_key_to_encrypt"));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] { new Claim("Sub", login.Email),
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
