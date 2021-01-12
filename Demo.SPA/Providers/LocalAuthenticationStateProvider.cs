using Blazored.LocalStorage;
using Demo.Shared.Models.User;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.SPA.Providers
{
    public class LocalAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly ILocalStorageService _storageService;

        public LocalAuthenticationStateProvider(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storageService.ContainKeyAsync("User"))
            {
                var userInfo = await _storageService.GetItemAsync<LoginResponse>("User");

                var claims = ParseClaimsFromJwt(userInfo.Token);

                var identity = new ClaimsIdentity(claims, "BearerToken");
                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);
                
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }   

        /// <summary>
        /// Login and Notify
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public async Task LogInAsync(LoginResponse userInfo)
        {
            await _storageService.SetItemAsync<LoginResponse>("User", userInfo);
            await GetAuthenticationStateAsync();
        }

        /// <summary>
        /// Logout and Notify
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            await _storageService.RemoveItemAsync("User");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }

        /// <summary>
        /// Parse Claims From Jwt Token
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);

            JsonSerializerOptions op = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                MaxDepth = 5
            };

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes, op);

            foreach (var kvp in keyValuePairs)
            {
                var typ = ((JsonElement)kvp.Value);
                switch (typ.ValueKind)
                {
                    case JsonValueKind.Array:
                        foreach (var r in typ.EnumerateArray())
                        {
                            claims.Add(new Claim(kvp.Key, r.ToString()));
                        }
                        break;

                    case JsonValueKind.String:
                        claims.Add(new Claim(kvp.Key, kvp.Value.ToString()));
                        break;
                }
            }         

            return claims;
        }

        /// <summary>
        /// Parse Base64 String
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
