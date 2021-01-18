using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Shared.Models.Role;

namespace Demo.SPA.Services.Security
{
    public interface ISecurityService
    {
        public Task<RolePermissionModel> AddRole(RolePermissionModel role);

        public Task<RolePermissionModel> GetRole(string id);

        public Task<IEnumerable<RoleResponse>> GetRoleNamesByIds(string id);

        public Task<IEnumerable<RoleResponse>> GetAllRole();

        public Task<bool> IsRoleExists(string name);

        public Task<bool> UpdateRole(RolePermissionModel role);

        public Task<bool> DeleteRole(string role);

        public List<RoleResponse> GetRoleForUsers();

        public Task<IEnumerable<string>> GetAllPermissions();
    }
}
