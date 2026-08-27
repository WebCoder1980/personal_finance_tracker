using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.ServiceDefaults.Constants;
using PersonalFinanceTracker.Users.Application.Handlers;
using PersonalFinanceTracker.Users.Application.Ports.In;
using PersonalFinanceTracker.Users.Infrastructure.Dtos;

namespace PersonalFinanceTracker.Users.Infrastructure.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRegisterHandler _userRegisterHandler;
    private readonly IUserLoginHandler _userLoginHandler;

    public AuthController(IUserRegisterHandler userRegisterHandler, IUserLoginHandler userLoginHandler)
    {
        _userRegisterHandler = userRegisterHandler;
        _userLoginHandler = userLoginHandler;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserRegisterResult>> Register(UserRegisterRequest request, CancellationToken token)
    {
        UserRegisterCommand command = new(request.UserName, request.Password, AppRoles.USER);
        UserRegisterResult result = await _userRegisterHandler.ExecuteAsync(command, token);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserLoginResult>> Login(UserLoginRequest request, CancellationToken token)
    {
        UserLoginCommand command = new(request.UserName, request.Password);
        UserLoginResult result = await _userLoginHandler.ExecuteAsync(command, token);
        return Ok(result);
    }
}