using Microsoft.AspNetCore.Mvc;
using Users.Dtos;
using Users.Service;

namespace Users.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        LoginResponse? result = await _service.Login(request);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
    {
        RegisterResponse? result = await _service.Register(request);
        return Ok(result);
    }
}