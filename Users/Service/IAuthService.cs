using Microsoft.AspNetCore.Identity.Data;
using Users.Dtos;
using LoginRequest = Users.Dtos.LoginRequest;
using RegisterRequest = Users.Dtos.RegisterRequest;

namespace Users.Service
{
    public interface IAuthService
    {
        Task<LoginResponse?> Login(LoginRequest request);
        Task<RegisterResponse?> Register(RegisterRequest request);
    }
}
