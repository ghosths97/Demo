using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using Demo.Shared.Models.Role;
using Demo.SPA.Services.Security;
using Demo.SPA.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Demo.SPA.Components
{
    partial class RoleDetail
    {
        [Parameter]
        public string roleId { get; set; }

        [Inject]
        public ISecurityService securityService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IToastService toastService { get; set; }

        private RoleViewModel role;

        private List<Permission> Permissions;

        [Parameter]
        public EventCallback Refresh { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(roleId))
            {
                var _role = await securityService.GetRole(roleId);
                var _permissons = await securityService.GetAllPermissions();

                Permissions = _permissons.Select(p => new Permission()
                {
                    Name = p,
                    IsSelected = _role.Permissions.Contains(p)
                }).ToList();

                role = new RoleViewModel()
                {
                    Id = _role.RoleId,
                    Name = _role.Name
                };
            }
        }

        private void Change(Permission per, bool isChecked)
        {
            for (int i = 0; i < Permissions.Count; i++)
            {
                if(Permissions[i].Name == per.Name)
                {
                    Permissions[i].IsSelected = isChecked;
                    break;
                }
            }
        }

        private void Save()
        {
            RolePermissionModel rolePermissionModel = new RolePermissionModel()
            {
                RoleId = role.Id,
                Name = role.Name,
                Permissions = this.Permissions.Where(p=>p.IsSelected).Select(p=>p.Name).ToArray()
            };
            securityService.UpdateRole(rolePermissionModel);
            toastService.ShowInfo("Role Updated Successfully.");
            Refresh.InvokeAsync();
        }

        private void Delete()
        {
            securityService.DeleteRole(role.Id);
            toastService.ShowWarning("Role Deleted Successfully.");
            Refresh.InvokeAsync();
        }
    }
}
