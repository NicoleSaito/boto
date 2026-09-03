using Microsoft.AspNetCore.Identity;

namespace Boto.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
}