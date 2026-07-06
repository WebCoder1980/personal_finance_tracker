using Microsoft.AspNetCore.Identity.Data;
using PersonalFinanceTracker.Domain.Dtos;
using LoginRequest = PersonalFinanceTracker.Domain.Dtos.LoginRequest;
using RegisterRequest = PersonalFinanceTracker.Domain.Dtos.RegisterRequest;

namespace PersonalFinanceTracker.Users.Service
{
    public interface IAuthService
    {
        Task<LoginResponse?> Login(LoginRequest request);
        Task<RegisterResponse?> Register(RegisterRequest request);
    }
}
