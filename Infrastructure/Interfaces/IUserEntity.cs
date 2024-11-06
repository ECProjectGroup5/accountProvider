namespace Infrastructure.Interfaces
{
    
    /// <summary>
    /// Represents a user entity that adds details to IdentityUser.
    /// </summary>
    /// <remarks>
    /// Contains properties for first name, last name, and a check if the account is external.
    /// /// </remarks>
    public interface IUserEntity
    {
        string FirstName { get; set; }
        bool IsExternalAccount { get; set; }
        string LastName { get; set; }
    }
}