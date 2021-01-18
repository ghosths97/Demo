using Demo.Shared.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.SPA.Services.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly HttpClient _client;

        public SecurityService(HttpClient client)
        {
            _client = client;
        }

        public async Task<RolePermissionModel> AddRole(RolePermissionModel role)
        {
            var response = await _client.PostAsJsonAsync<RolePermissionModel>("/roles", role);
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<RolePermissionModel>(json);
        }

        public async Task<bool> DeleteRole(string id)
        {
            await _client.DeleteAsync($"/roles/{id}");
            return true;
        }

        public async Task<IEnumerable<string>> GetAllPermissions()
        {
            return await _client.GetFromJsonAsync<IEnumerable<string>>("/roles/permissions");
        }

        public async Task<IEnumerable<RoleResponse>> GetAllRole()
        {
            return await _client.GetFromJsonAsync<IEnumerable<RoleResponse>>("/roles");
        }

        public async Task<RolePermissionModel> GetRole(string id)
        {
            return await _client.GetFromJsonAsync<RolePermissionModel>($"/roles/{id}");
        }

        public List<RoleResponse> GetRoleForUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleResponse>> GetRoleNamesByIds(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRoleExists(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRole(RolePermissionModel role)
        {
            _client.PutAsJsonAsync<RolePermissionModel>("/Roles", role);
            return Task.FromResult<bool>(true);
        }
    }
}
