using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Shared.Security
{
    /// <summary>
    /// Policy Constants
    /// </summary>
    public static class Policy
    {
        #region Policy Names

        public const string Users = "Users";

        public const string Login = "Login";

        public const string Role = "Role";

        public const string Permission = "Permissions";

        public const string AddProduct = "AddProduct";

        public const string EditProduct = "EditProduct";

        public const string ViewProduct = "ViewProduct";

        private static string[] AuthScheme = new string[] { "Bearer" };

        #endregion

        #region Policy Definitions
        public static AuthorizationPolicy UsersPolicy()
            => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes(AuthScheme)
            .AddRequirements(new PermissionRequirement(Users))
            .Build();

        public static AuthorizationPolicy LoginPolicy()
            => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes(AuthScheme)
            .AddRequirements(new PermissionRequirement(Login))
            .Build();
        
        public static AuthorizationPolicy RolePolicy()
            => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes(AuthScheme)
            .AddRequirements(new PermissionRequirement(Role))
            .Build();

        public static AuthorizationPolicy PermissionPolicy()
            => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes(AuthScheme)
            .AddRequirements(new PermissionRequirement(Permission))
            .Build();

        public static AuthorizationPolicy AddProductPolicy()
            => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes(AuthScheme)
            .AddRequirements(new PermissionRequirement(AddProduct))
            .Build();

        public static AuthorizationPolicy EditProductPolicy()
            => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes(AuthScheme)
            .AddRequirements(new PermissionRequirement(EditProduct))
            .Build();

        #endregion
    }
}
