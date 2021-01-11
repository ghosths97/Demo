using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Shared.Models.User;
using Demo.SPA.Services.Authentication;
using Microsoft.AspNetCore.Components;

namespace Demo.SPA.Pages.Auth
{
    [Route("auth/register")]
    partial class Register
    {
        [Inject]
        public AuthenticationService authenticationService { get; set; }
            
        private RegisterRequest registerRequest;

        public Register()
        {
            registerRequest = new RegisterRequest();
        }

        private async Task RegisterUser()
        {
            var res = await authenticationService.RegisterUserAsync(registerRequest);
            
            if (res != null)
            {

            }
        }
    }
}
