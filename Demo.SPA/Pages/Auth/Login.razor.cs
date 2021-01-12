using Demo.Shared.Models.User;
using Demo.SPA.Providers;
using Demo.SPA.Services.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Auth
{

    partial class Login
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IAuthenticationService authenticationService { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; }

        private LoginRequest loginRequest;

        [Parameter]
        public string returnUrl { get; set; }

        public Login()
        {
            loginRequest = new LoginRequest();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        public async Task LoginUser()
        {
            var res = await authenticationService.LoginUserAsync(loginRequest);
            if (res != null)
            {
                await ((LocalAuthenticationStateProvider)authenticationStateProvider).LogInAsync(res);
                returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
                navigationManager.NavigateTo(returnUrl);
            }
        }
    }
}
