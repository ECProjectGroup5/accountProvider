using accountProvider.Controllers;
using accountProvider.ViewModels;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace accountProviderTests;

public class AuthControllerTests
{
    private readonly Mock<UserManager<UserEntity>> _userManagerMock;
    private readonly Mock<SignInManager<UserEntity>> _signInManagerMock;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        // Skapar mock för UserManager och SignInManager
        _userManagerMock = new Mock<UserManager<UserEntity>>(
            new Mock<IUserStore<UserEntity>>().Object, null, null, null, null, null, null, null, null);

        _signInManagerMock = new Mock<SignInManager<UserEntity>>(
            _userManagerMock.Object,
            new Mock<IHttpContextAccessor>().Object,
            new Mock<IUserClaimsPrincipalFactory<UserEntity>>().Object,
            null, null, null, null);

        // Skapar ny instans av AuthController med mockade beroenden
        _controller = new AuthController(_userManagerMock.Object, _signInManagerMock.Object, null);
    }

    [Fact]
    public async Task SignIn_ShouldReturnValidationError_IfEmailIsEmpty()
    {
        // Arrange
        var viewModel = new SignInViewModel
        {
            Email = "", // Lämnar epost tomt för att trigga validering
            Password = "somePassword123",
            RememberMe = false
        };

        // Act
        var result = await _controller.SignIn(viewModel) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Incorrect credentials, try again.", result.ViewData["StatusMessage"]);
    }
    [Fact]
    public async Task SignIn_ShouldReturnValidationError_IfPasswordIsEmpty()
    {
        // Arrange
        var viewModel = new SignInViewModel
        {
            Email = "test@domain.com",
            Password = "",
            RememberMe = false
        };

        // Act
        var result = await _controller.SignIn(viewModel) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Incorrect credentials, try again.", result.ViewData["StatusMessage"]);
    }
    [Fact]
    public async Task SignIn_ShouldRedirectToAccountIndex_OnSuccessfulLogin()
    {
        // Arrange
        var viewModel = new SignInViewModel
        {
            Email = "test@example.com",
            Password = "somePassword123",
            RememberMe = true
        };

        var user = new UserEntity { Email = viewModel.Email };
        _userManagerMock.Setup(x => x.FindByEmailAsync(viewModel.Email)).ReturnsAsync(user);
        _signInManagerMock.Setup(x => x.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false))
            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

        // Act
        var result = await _controller.SignIn(viewModel);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Account", redirectResult.ControllerName);
    }
}
