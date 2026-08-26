using PersonalFinanceTracker.Users.Domain.Constants;
using PersonalFinanceTracker.Users.Domain.Exceptions;
using PersonalFinanceTracker.Users.Domain.Models;
using Xunit.Internal;

namespace PersonalFinanceTracker.Users.Domain.Tests;

public class UserTests
{
    [Fact]
    public void Test_Register_User_Ok()
    {
        // Arrange
        const string userName = "maxsmg", passwordHash = "<Hash>", role = AppRoles.USER;

        // Act
        User user = User.Register(userName, passwordHash, role);

        // Assert
        Assert.Equal(userName, user.UserName);
    }

    [Fact]
    public void Test_Register_Admin_Ok()
    {
        // Arrange
        const string userName = "admin", passwordHash = "<Hash>", role = AppRoles.ADMIN;

        // Act
        User user = User.Register(userName, passwordHash, role);

        // Assert
        Assert.Equal(userName, user.UserName);
    }

    [Fact]
    public void Test_Register_User_UserName_Empty()
    {
        // Arrange
        const string userName = "   ", passwordHash = "<Hash>", role = AppRoles.USER;

        // Act
        DomainException? message = Assert.Throws<DomainException>(() => User.Register(userName, passwordHash, role));

        // Assert
        Assert.Equal("UserName cannot be empty", message.Message);
    }

    [Fact]
    public void Test_Register_User_UserName_Too_short()
    {
        // Arrange
        const string userName = "W", passwordHash = "<Hash>", role = AppRoles.USER;

        // Act
        DomainException? message = Assert.Throws<DomainException>(() => User.Register(userName, passwordHash, role));

        // Assert
        Assert.Equal("UserName must be between 5 and 50 chars long", message.Message);
    }

    [Fact]
    public void Test_Register_User_UserName_Too_long()
    {
        // Arrange
        const string passwordHash = "<Hash>", role = AppRoles.USER;

        string userName = "";
        for (int i = 0; i < 51; i++)
        {
            userName += "W";
        }

        // Act
        DomainException? message = Assert.Throws<DomainException>(() => User.Register(userName, passwordHash, role));

        // Assert
        Assert.Equal("UserName must be between 5 and 50 chars long", message.Message);
    }

    [Fact]
    public void Test_Register_User_PasswordHash_Empty()
    {
        // Arrange
        const string userName = "maxsmg", passwordHash = " ", role = AppRoles.USER;

        // Act
        DomainException? message = Assert.Throws<DomainException>(() => User.Register(userName, passwordHash, role));

        // Assert
        Assert.Equal("PasswordHash cannot be empty", message.Message);
    }

    [Fact]
    public void Test_Register_Role_Is_invalid()
    {
        // Arrange
        const string userName = "maxsmg", passwordHash = "<Hash>", role = "wrong";

        // Act
        DomainException? message = Assert.Throws<DomainException>(() => User.Register(userName, passwordHash, role));

        // Assert
        Assert.Equal("Role is invalid", message.Message);
    }
}
