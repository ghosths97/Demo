using Blazored.Toast.Services;
using Demo.Shared.Models.Role;
using Demo.Shared.Models.User;
using Demo.SPA.Services.Security;
using Demo.SPA.Services.User;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Admin.Users
{
    [Route("/admin/user/detail/{id}")]
    partial class User
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IUserService userService { get; set; }
      
        [Inject]
        public ISecurityService securityService { get; set; }

        [Inject]
        public IToastService toastService { get; set; }

        private UserDto user;

        private IEnumerable<RoleResponse> roles;

        protected override async Task OnInitializedAsync()
        {
            user = await userService.GetUserAsync(Id);
            roles = await securityService.GetAllRole();
        }

        private async Task Save()
        {
            await userService.UpdateUserAsync(user);
            toastService.ShowInfo("User Updated Successfully.");
        }
    }
}
