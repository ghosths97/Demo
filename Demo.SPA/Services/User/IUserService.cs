using Demo.Shared.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.SPA.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync();

        Task<UserDto> GetUserAsync(string Id);

        Task<UserDto> UpdateUserAsync(UserDto user);
    }
}