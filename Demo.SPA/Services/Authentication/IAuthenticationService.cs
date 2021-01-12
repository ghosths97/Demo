using Demo.Shared.Models.User;
using System.Threading.Tasks;

namespace Demo.SPA.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginUserAsync(LoginRequest request);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
    }
}