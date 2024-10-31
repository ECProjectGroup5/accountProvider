using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

/// <summary>
/// Represents a user entity that adds details to IdentityUser.
/// </summary>
/// <remarks>
/// Contains properties for first name, last name, and a check if the account is external.
/// /// </remarks>
public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    public bool IsExternalAccount { get; set; } = false;
}