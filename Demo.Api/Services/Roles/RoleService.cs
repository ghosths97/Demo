using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.Api.Models.Identity;
using Demo.Persistence;
using Demo.Shared.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Demo.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly DemoDbContext _context;
        
        public RoleService(DemoDbContext context, RoleManager<Role> roleManager,
                                UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<RolePermissionModel> AddRole(RolePermissionModel roleRequest)
        {
            Role role = new Role();
            role.Name = roleRequest.Name.Trim();

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                foreach (var permission in roleRequest.Permissions)
                {
                    await _roleManager.AddClaimAsync(role, new Claim("permission", permission));
                }
                roleRequest.RoleId = role.Id;
                return roleRequest;

            }
            return null;
        }

        public async Task<RolePermissionModel> GetRole(string id)
        {
            RolePermissionModel model = new RolePermissionModel();
            var applicationRole = await _roleManager.Roles.Select(x => new Role()
            {
                Id = x.Id,
                Name = x.Name,
            }).FirstOrDefaultAsync(s => s.Id == id);
            if (applicationRole != null)
            {
                model.Name = applicationRole.Name;
                model.RoleId = applicationRole.Id;
                var claims = await _roleManager.GetClaimsAsync(applicationRole);
                var permission = claims.Select(s => s.Value).ToArray();
                model.Permissions = permission;
            }
            return model;

        }

        public async Task<IList<RolePermissionModel>> GetAllRole()
        {
            IList<RolePermissionModel> models = new List<RolePermissionModel>();
            try
            {
                var applicationRoles = _roleManager.Roles.ToList();

                foreach (var role in applicationRoles)
                {
                    var claims = await _roleManager.GetClaimsAsync(role);
                    if (claims == null)
                    {
                        models.Add(new RolePermissionModel
                        {
                            Name = role.Name,
                            RoleId = role.Id
                        });
                    }
                    else
                    {
                        var permission = claims.Select(s => s.Value).ToArray();
                        models.Add(new RolePermissionModel
                        {
                            Name = role.Name,
                            RoleId = role.Id,
                            Permissions = permission
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return models;

            }
            return models;

        }

        public async Task<bool> IsRoleExists(string name)
        {
            if (name != null)
            {
                return await _roleManager.RoleExistsAsync(name.Trim());
            }
            return false;
        }

        public async Task<bool> UpdateRole(RolePermissionModel roleRequest)
        {
            if (roleRequest.RoleId == null)
            {
                return false;
            }
            var role = await _roleManager.FindByIdAsync(roleRequest.RoleId);
            if (role != null)
            {
                role.Name = roleRequest.Name.Trim();

                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    var claims = await _roleManager.GetClaimsAsync(role);
                    var claimsPermissionList = claims.Select(s => s.Value).ToArray();
                    foreach (var claimPermission in claimsPermissionList)
                    {
                        if (!roleRequest.Permissions.Contains(claimPermission))
                        {
                            await _roleManager.RemoveClaimAsync(role, new Claim("permission", claimPermission));
                        }
                    }

                    foreach (var permission in roleRequest.Permissions)
                    {
                        if (!claimsPermissionList.Contains(permission))
                        {
                            await _roleManager.AddClaimAsync(role, new Claim("permission", permission));
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public List<Role> GetRoleForUsers()
        {
            return _roleManager.Roles.ToList();
        }

        public List<string> GetAllPermissions()
        {
            return _context.Permissions.Select(m => m.Name).ToList();
        }

        public async Task<IList<RoleResponse>> GetRoleNamesByIds(string id)
        {
            var ids = id.Split(",");
            IList<RoleResponse> result = new List<RoleResponse>();
            for (int i = 0; i < ids.Length; i++)
            {
                var name = await _roleManager.Roles.Select(s => new Role() { Id = s.Id, Name = s.Name })
                .FirstOrDefaultAsync(e => e.Id == ids[i]);
                if (name != null)
                {
                    result.Add(new RoleResponse { RoleId = ids[i], Name = name.Name });
                }
            }
            var finalresult = result.Distinct().ToList();
            return finalresult;
        }   

        public async Task<bool> DeleteRole(string roleID)
        {
            var role = _roleManager.Roles.Where(r => r.Id == roleID).FirstOrDefault();
            if (role != null)
                await _roleManager.DeleteAsync(role);

            return true;
        }
    }
}
