using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

public class RefreshToken(int? Id, string Token, DateTime Created_at, DateTime Expires_at, Guid? Userid)
{
    public int? id { get; set; } = Id;
    public string? token { get; set; } = Token;
    public DateTime? created_at { get; set; } = Created_at;
    public DateTime? expires_at { get; set; } = Expires_at;
    public Guid? userid { get; set; } = Userid;
        public static string GenerateRefreshToken() {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
    };

