namespace Boto.Application.Features.Auth.Login;

public record LoginResult(bool Success, string? Token, string? ErrorMessage);