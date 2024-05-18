using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Auth;

public sealed class User : IdentityUser<int>
{
    private User()
    {
    }
    
    public void SetEmail(string email)
    {
        if (!MailAddress.TryCreate(email, out _))
            throw new ArgumentException();

        Email = email;
    }

    public void SetPassword(string password)
    {
        // ArgumentException.ThrowIfNullOrEmpty(password);
        // PasswordHash = password;
    }

    public static User Create(string email, string name)
    {
        var user = new User
        {
            Email = email,
            UserName = name
        };

        return user;
    }
}