using Demo.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Shared.Helpers
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }

            // Get all the roles the user belongs to and check if any of the roles has the permission required
            // for the authorization to succeed.
            var claims = context.User.Claims.ToArray();

            if (claims.Any(x => x.Type == "permission" && x.Value == requirement.Permission))
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}