using Microsoft.AspNetCore.Identity;
using Boto.Application.Interfaces;
using Boto.Domain.Entities;

namespace Boto.Application.Features.Auth.Login;

public class LoginCommandHandler
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<LoginResult> HandleAsync(LoginCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            return new LoginResult(false, null, "E-mail ou senha inválidos.");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, command.Password);
        if (!isPasswordValid)
        {
            return new LoginResult(false, null, "E-mail ou senha inválidos.");
        }

        var token = _tokenService.GenerateToken(user);
        return new LoginResult(true, token, null);
    }
}