using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using Boto.Application.Features.Auth.Login;
using Boto.Application.Interfaces;
using Boto.Domain.Entities;

namespace Boto.Application.Tests.Features.Auth.Login;

public class LoginCommandHandlerTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        var storeMock = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(storeMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        _tokenServiceMock = new Mock<ITokenService>();
        _handler = new LoginCommandHandler(_userManagerMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_UsuarioNaoExiste_RetornaFalha()
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        var command = new LoginCommand("naoexiste@teste.com", "qualquerSenha");

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("E-mail ou senha inválidos.", result.ErrorMessage);
    }

    [Fact]
    public async Task HandleAsync_SenhaIncorreta_RetornaFalha()
    {
        // Arrange
        var user = new User { Email = "nicole@teste.com", Name = "Nicole" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, It.IsAny<string>()))
            .ReturnsAsync(false);

        var command = new LoginCommand(user.Email, "senhaErrada");

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("E-mail ou senha inválidos.", result.ErrorMessage);
    }

    [Fact]
    public async Task HandleAsync_CredenciaisValidas_RetornaTokenComSucesso()
    {
        // Arrange
        var user = new User { Email = "nicole@teste.com", Name = "Nicole" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, "senhaCorreta"))
            .ReturnsAsync(true);
        _tokenServiceMock.Setup(x => x.GenerateToken(user))
            .Returns("token-fake-gerado");

        var command = new LoginCommand(user.Email, "senhaCorreta");

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("token-fake-gerado", result.Token);
    }
}