using Microsoft.AspNetCore.Identity;

public class PassowrdHasher
{
    private readonly PasswordHasher<object> _hasher = new();

    public string HashPassword(string password)
    {
        return _hasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string hashedPassword, string plaintPassword)
    {
        var result = _hasher.VerifyHashedPassword(null, hashedPassword, plaintPassword);
        return result == PasswordVerificationResult.Success;
    }
}