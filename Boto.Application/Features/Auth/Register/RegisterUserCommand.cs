namespace Boto.Application.Features.Auth.Register;

public record RegisterUserCommand(string Name, string Email, string Password);