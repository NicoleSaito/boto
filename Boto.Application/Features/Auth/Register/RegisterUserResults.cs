namespace Boto.Application.Features.Auth.Register;

public record RegisterUserResult(bool Success, string? Token, IEnumerable<string>? Errors);