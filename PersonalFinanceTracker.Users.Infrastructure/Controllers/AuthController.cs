using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Users.Application.Handlers;
using PersonalFinanceTracker.Users.Application.Ports.In;
using PersonalFinanceTracker.Users.Infrastructure.Dtos;

namespace PersonalFinanceTracker.Users.Infrastructure.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRegisterHandler _userRegisterHandler;

    public AuthController(IUserRegisterHandler userRegisterHandler)
    {
        _userRegisterHandler = userRegisterHandler;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserRegisterResult>> Register(UserRegisterRequest request, CancellationToken token)
    {
        UserRegisterCommand command = new(request.UserName, request.Password);
        UserRegisterResult result = await _userRegisterHandler.ExecuteAsync(command, token);
        return Ok(result);
    }
}