using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/login")]

public class LoginController : ControllerBase
{
    private readonly AppDbContext _context;

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] User data)
    {
        try
        {
            var results = await _context.users.Where(user => user.email == data.email).ToListAsync();
            if (results.Count == 0)
            {
                return StatusCode(404, new { error = "Email not found " });
            }

            var passwordService = new PassowrdHasher();
            var check = passwordService.VerifyPassword(results[0].password, data.password);
            if (check)
            {
                var token = RefreshToken.GenerateRefreshToken();
                var accessToken = AccessToken.GenerateAccessToken(results[0].id.ToString() ?? "");

                var refreshToken = new RefreshToken { token = token, created_at = DateTime.UtcNow, expires_at = DateTime.UtcNow.AddHours(3), userid = results[0].id };
                try
                {
                    _context.refresh_tokens.Add(refreshToken);
                    await _context.SaveChangesAsync();

                    Response.Cookies.Append("refreshtoken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddDays(7)
                    });
                    Response.Cookies.Append("accesstoken", accessToken, new CookieOptions
                    {
                        HttpOnly = false,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddMinutes(15)
                    });

                    return Ok(new { message = "User logged in" , accessToken});
                }
                catch (Exception e)
                {
                    return StatusCode(500, new { error = e.InnerException });
                }
            }

            return StatusCode(500, new { error = "Incorrect password" });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.InnerException });
        }
    }

    public LoginController(AppDbContext context)
    {
        _context = context;
    }
}
