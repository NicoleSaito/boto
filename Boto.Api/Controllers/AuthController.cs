using Microsoft.AspNetCore.Mvc;
using Boto.Application.Features.Auth.Register;
using Boto.Application.Features.Auth.Login;

namespace Boto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserCommandHandler _registerHandler;
    private readonly LoginCommandHandler _loginHandler;

    public AuthController(RegisterUserCommandHandler registerHandler, LoginCommandHandler loginHandler)
    {
        _registerHandler = registerHandler;
        _loginHandler = loginHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _registerHandler.HandleAsync(command);

        if (!result.Success)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return Ok(new { token = result.Token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _loginHandler.HandleAsync(command);

        if (!result.Success)
        {
            return Unauthorized(new { message = result.ErrorMessage });
        }

        return Ok(new { token = result.Token });
    }
}