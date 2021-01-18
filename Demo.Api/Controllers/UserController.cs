using Demo.Api.Models.Identity;
using Demo.Exceptions;
using Demo.Shared.Models.User;
using Demo.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Policy = Policy.Users)]
        [Route("all")]
        public IActionResult GetAll()
        {
            var users = _userManager.Users.Select(u => new UserDto()
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Phonenumber = u.PhoneNumber,
                
            }).ToList();
            return Ok(users);
        }

        [HttpGet]
        [Authorize(Policy = Policy.Users)]
        public async Task<IActionResult> Get(string Id)
        {
            var user = await _userManager.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new DemoException("User not found", System.Net.HttpStatusCode.NotFound);
            }
            var roles = await _userManager.GetRolesAsync(user);

            var response = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phonenumber = user.PhoneNumber,
                Role = roles.FirstOrDefault()
            };

            return Ok(response);
        }

        [HttpPut]
        [Authorize(Policy = Policy.Users)]
        public async Task<IActionResult> Put(UserDto userDto)
        {
            var user = await _userManager.Users.Where(u => u.Id == userDto.Id).FirstOrDefaultAsync();
            user.PhoneNumber = userDto.Phonenumber;
            user.Name = userDto.Name;
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.UpdateAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, userDto.Role);
            var response = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phonenumber = user.PhoneNumber
            };

            return Ok(response);
        }
    }
}
