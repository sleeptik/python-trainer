﻿using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Auth;

public sealed class User : IdentityUser<int>
{
    private User()
    {
    }

    public int Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;

    public void SetEmail(string email)
    {
        if (!MailAddress.TryCreate(email, out _))
            throw new ArgumentException();

        Email = email;
    }

    public void SetPassword(string password)
    {
        ArgumentException.ThrowIfNullOrEmpty(password);
        Password = password;
    }

    public static User Create(string email, string password)
    {
        var user = new User();

        user.SetEmail(email);
        user.SetPassword(password);

        return user;
    }
}