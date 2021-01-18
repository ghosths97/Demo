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
        /// <summary>
        /// Add Authorization With Permissions (Policy)
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthorizationWithPermissions(this IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.InvokeHandlersAfterFailure = false;

                options.AddPolicy(Policy.Users, Policy.UsersPolicy());
                options.AddPolicy(Policy.AddProduct, Policy.AddProductPolicy());
                options.AddPolicy(Policy.EditProduct, Policy.EditProductPolicy());
                options.AddPolicy(Policy.Permission, Policy.PermissionPolicy());
                options.AddPolicy(Policy.Role, Policy.RolePolicy());
                options.AddPolicy(Policy.Login, Policy.LoginPolicy());

            });
        }
    }
}
