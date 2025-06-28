using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

    public class RefreshToken
    {
        public int? id { get; set; }
        public string? token { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? expires_at { get; set; }
        public Guid? userid { get; set; }
    };

public static class Generator
{
    public static string GenerateRefreshToken() {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

}