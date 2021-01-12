using Demo.Shared.Helpers;
using Demo.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Shared.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddAuthorizationWithPermissions(this IServiceCollection services)
        {
            var AuthScheme = new string[]{ "Bearer" };

            services.AddAuthorizationCore(options =>
            {
                options.InvokeHandlersAfterFailure = false;

                options.AddPolicy(Permissions.Login, policy => { policy.AuthenticationSchemes = AuthScheme; policy.AddRequirements(new PermissionRequirement(Permissions.Login)); });
                
                options.AddPolicy(Permissions.Users, policy => { policy.AuthenticationSchemes = AuthScheme; policy.AddRequirements(new PermissionRequirement(Permissions.Users)); });
                
                options.AddPolicy(Permissions.AddProduct, policy => { policy.AuthenticationSchemes = AuthScheme; policy.AddRequirements(new PermissionRequirement(Permissions.AddProduct)); });
                options.AddPolicy(Permissions.EditProduct, policy => { policy.AuthenticationSchemes = AuthScheme; policy.AddRequirements(new PermissionRequirement(Permissions.EditProduct)); });
                options.AddPolicy(Permissions.ViewProduct, policy => { policy.AuthenticationSchemes = AuthScheme; policy.AddRequirements(new PermissionRequirement(Permissions.ViewProduct)); });
                options.AddPolicy(Permissions.Permission, policy => { policy.AuthenticationSchemes = AuthScheme; policy.AddRequirements(new PermissionRequirement(Permissions.Permission)); });
                options.AddPolicy(Permissions.Role, policy => { policy.AuthenticationSchemes = AuthScheme; policy.AddRequirements(new PermissionRequirement(Permissions.Role)); });
                
            });
        }
    }
}
