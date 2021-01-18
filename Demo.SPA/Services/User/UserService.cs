using Demo.Shared.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;

namespace Demo.SPA.Services.User
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            return await _client.GetFromJsonAsync<IEnumerable<UserDto>>("user/all");
        }

        public async Task<UserDto> GetUserAsync(string Id)
        {
            return await _client.GetFromJsonAsync<UserDto>($"user?Id={Id}");
        }

        public async Task<UserDto> UpdateUserAsync(UserDto user)
        {
            var response = await _client.PutAsJsonAsync<UserDto>("user", user);
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UserDto>(json);
        }
    }
}
