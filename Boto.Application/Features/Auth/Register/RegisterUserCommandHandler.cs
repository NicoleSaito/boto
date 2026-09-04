using Microsoft.AspNetCore.Identity;
using Boto.Application.Interfaces;
using Boto.Domain.Entities;

namespace Boto.Application.Features.Auth.Register;

public class RegisterUserCommandHandler
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public RegisterUserCommandHandler(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<RegisterUserResult> HandleAsync(RegisterUserCommand command)
    {
        var user = new User
        {
            UserName = command.Email,
            Email = command.Email,
            Name = command.Name
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            return new RegisterUserResult(false, null, result.Errors.Select(e => e.Description));
        }

        var token = _tokenService.GenerateToken(user);
        return new RegisterUserResult(true, token, null);
    }
}