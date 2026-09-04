using Boto.Domain.Entities;

namespace Boto.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}