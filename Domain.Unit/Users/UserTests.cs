using Domain.Users;

namespace Domain.Unit.Users;

public class UserTests
{
    private readonly User _exampleUser = User.Create("example@mail.com", "examplePassword");

    [Fact]
    public void UserCreate_ValidData_CreatesUser()
    {
        // Arrange
        const string email = "example@mail.com";
        const string password = "examplePassword";

        // Act
        var user = User.Create(email, password);

        // Assert
        user.Email.Should().Be(email);
        user.Password.Should().Be(password);
    }

    [Fact]
    public void UserCreate_InvalidEmail_CreatesUser()
    {
        // Arrange
        const string email = "examplemail.com";
        const string password = "examplePassword";

        // Act & Assert
        var action = () => User.Create(email, password);
        action.Invoking(func => func.Invoke()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UserCreate_InvalidPassword_CreatesUser()
    {
        // Arrange
        const string email = "example@mail.com";
        const string password = "";

        // Act & Assert
        var action = () => User.Create(email, password);
        action.Invoking(func => func.Invoke()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SetEmail_ValidEmail_SetsEmail()
    {
        // Arrange
        const string email = "example@mail.com";

        // Act
        _exampleUser.SetEmail(email);

        // Assert
        _exampleUser.Email.Should().Be(email);
    }

    [Fact]
    public void SetEmail_InvalidEmail_ThrowsException()
    {
        // Arrange
        const string email = "example23432";

        // Act & Assert
        _exampleUser.Invoking(user1 => user1.SetEmail(email)).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SetEmail_EmailIsEmpty_ThrowsException()
    {
        // Arrange
        const string email = "";

        // Act & Assert
        _exampleUser.Invoking(user1 => user1.SetEmail(email)).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SetEmail_EmailIsNull_ThrowsException()
    {
        // Arrange
        const string email = null!;

        // Act & Assert
        _exampleUser.Invoking(user1 => user1.SetEmail(email)).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SetPassword_ValidPassword_SetsPassword()
    {
        // Arrange
        const string password = "password";

        // Act
        _exampleUser.SetPassword(password);

        // Assert
        _exampleUser.Password.Should().Be(password);
    }

    [Fact]
    public void SetPassword_PasswordIsEmpty_ThrowsException()
    {
        // Arrange
        const string password = "";

        // Act & Assert
        _exampleUser.Invoking(user1 => user1.SetPassword(password)).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SetPassword_PasswordIsNull_ThrowsException()
    {
        // Arrange
        const string password = null!;

        // Act & Assert
        _exampleUser.Invoking(user1 => user1.SetPassword(password!)).Should().Throw<ArgumentException>();
    }
}