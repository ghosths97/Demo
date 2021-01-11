using Blazored.LocalStorage;
using Demo.Shared.Models.User;
using Demo.SPA.Models;
using Demo.SPA.Services.Authentication;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Pages.Auth
{
    partial class Login
    {
        [Inject]
        public ILocalStorageService storageService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public AuthenticationService authenticationService { get; set; }

        private LoginRequest loginRequest;

        public Login()
        {
            loginRequest = new LoginRequest();
        }

        public async Task LoginUser()
        {
            var res = await authenticationService.LoginUserAsync(loginRequest);
            if (res != null)
            {
                await storageService.SetItemAsync<LoginResponse>("User", res);
            }
        }
    }
}
