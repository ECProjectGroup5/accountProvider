using Infrastructure.Entities;

namespace accountProviderTests;

public class UserEntityTests
{
    [Fact]
    public void UserEntity_DefaultConstructor_SetsDefaults()
    {
        // Arrange
        var user = new UserEntity();

        // Act & Assert
        Assert.NotNull(user);
        Assert.IsType<UserEntity>(user);
        Assert.False(user.IsExternalAccount, "IsExternalAccount should default to false.");
        Assert.Null(user.FirstName);
        Assert.Null(user.LastName);
        Assert.Null(user.Email); // 
    }

    [Fact]
    public void UserEntity_SetProperties_StoresValues()
    {
        // Arrange
        var user = new UserEntity
        {
            FirstName = "Jimmy",
            LastName = "Sjo",
            IsExternalAccount = true,
            Email = "jimmy.sjo@domain.com"
        };

        // Act & Assert
        Assert.Equal("Jimmy", user.FirstName);
        Assert.Equal("Sjo", user.LastName);
        Assert.True(user.IsExternalAccount);
        Assert.Equal("jimmy.sjo@domain.com", user.Email);
    }

    [Fact]
    public void UserEntity_IdentityUserProperties_WorkCorrectly()
    {
        // Arrange
        var user = new UserEntity
        {
            UserName = "jimmysjo",
            Email = "jimmysjo@domain.com",
            PhoneNumber = "123-456-789"
        };

        // Act & Assert
        Assert.Equal("jimmysjo", user.UserName);
        Assert.Equal("jimmysjo@domain.com", user.Email);
        Assert.Equal("123-456-789", user.PhoneNumber);
    }


}
