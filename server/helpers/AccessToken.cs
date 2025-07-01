using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

public static class AccessToken
{
    public static string GenerateAccessToken(string userid)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("QmCFv5yd1tiDf14p+pEpUyhA50vBYehAWMGqrOgBrOE="));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var desc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userid),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
        SigningCredentials = creds
        };
        var token = handler.CreateToken(desc);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}