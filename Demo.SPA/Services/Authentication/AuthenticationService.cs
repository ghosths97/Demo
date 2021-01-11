using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Demo.SPA.Util;
using Demo.Shared.Models.User;

namespace Demo.SPA.Services.Authentication
{
    public class AuthenticationService
    {
        private HttpClient _Client { get; }

        public AuthenticationService(HttpClient httpClient)
        {
            _Client = httpClient;
        }

        /// <summary>
        /// register
        /// </summary>
        /// <param name="request"></param>
        /// <returns>RegisterResponse</returns>
        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request)
        {
            var requestJson = new JsonContent(request);

            var response = await _Client.PostAsync($"/register", requestJson);

            var json = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<RegisterResponse>(json);
        }

        /// <summary>
        /// login in
        /// </summary>
        /// <param name="request"></param>
        /// <returns>LoginResponse</returns>
        public async Task<LoginResponse> LoginUserAsync(LoginRequest request)
        {
            JsonSerializerOptions op = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var requestJson = new JsonContent(request);
            var response = await _Client.PostAsync($"/login", requestJson);
        
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LoginResponse>(json, op);
            return result;
        }
    }
}
