using Demo.Api.Models.Identity;
using Demo.Shared.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services.Roles
{
    public interface IRoleService
    {
        public Task<RolePermissionModel> AddRole(RolePermissionModel role);
        
        public Task<RolePermissionModel> GetRole(string id);

        public Task<IList<RoleResponse>> GetRoleNamesByIds(string id);       
        
        public Task<IList<RolePermissionModel>> GetAllRole();
        
        public Task<bool> IsRoleExists(string name);
        
        public Task<bool> UpdateRole(RolePermissionModel role);

        public List<Role> GetRoleForUsers();

        public List<string> GetAllPermissions();
    }
}
