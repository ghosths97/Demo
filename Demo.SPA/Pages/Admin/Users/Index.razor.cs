using Demo.Shared.Models.User;
using Demo.Shared.Security;
using Demo.SPA.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Admin.Users
{
    [Route("admin/users")]
    [Authorize(Policy.Users)]
    partial class Index
    {
        private bool loading { get; set; }
        
        [Inject]
        private IUserService _userService { get; set; }

        private IEnumerable<UserDto> users;

        protected override async Task OnInitializedAsync()
        {
            users = await _userService.GetAllUserAsync();
        }
    }
}
