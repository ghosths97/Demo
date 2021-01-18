using Blazored.Toast.Services;
using Demo.Shared.Models.Role;
using Demo.Shared.Security;
using Demo.SPA.Components;
using Demo.SPA.Services.Security;
using Demo.SPA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Admin.Roles
{

    [Route("admin/roles")]
    //[Authorize(Policy.Role)]
    partial class Index
    {
        [Inject]
        public ISecurityService securityService { get; set; }

        [Inject]
        public IToastService toastService { get; set; }

        private IEnumerable<RoleResponse> roles;
        
        private string selectedRole;

        private RolePermissionModel roleRequest =new RolePermissionModel();

        private Modal modal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Refresh();
        }

        private async Task Refresh()
        {
            roles = null;
            selectedRole = "";
            roles = await securityService.GetAllRole();
            StateHasChanged();
        }

        private async Task Save()
        {
            if (string.IsNullOrEmpty(roleRequest.RoleId))
            {
                roleRequest.Permissions = new string[] { };
                await securityService.AddRole(roleRequest);
                toastService.ShowSuccess("Role Added Successfully.");
            }
            else
            {
                await securityService.UpdateRole(roleRequest);
                toastService.ShowInfo("Role Updated Successfully.");
            }

            modal.Close();
            await Refresh();
        }

        public async Task Change(string id)
        {
            roleRequest = roles.Where(r => r.RoleId == id).Select(r=>new RolePermissionModel()
            {
                RoleId = r.RoleId,
                Name = r.Name,
                Permissions = r.Permissions
            }).FirstOrDefault();
            modal.Open();
        }
    }
}
