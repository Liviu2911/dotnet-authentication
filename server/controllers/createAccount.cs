using System.Data.SqlTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/register")]
public class CreateAccountController : ControllerBase
{
    private readonly AppDbContext _context;
    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] User user)
    {
        var service = new PassowrdHasher();
        string hashedPassword = service.HashPassword(user.password != null ? user.password : "");
        Guid id = Guid.NewGuid();
        var newUser = new User { id=id, username=user.username, password=hashedPassword, email=user.email };
        var token = RefreshToken.GenerateRefreshToken();
        var refresh_token = new RefreshToken { token = token, created_at = DateTime.UtcNow, expires_at=DateTime.UtcNow.AddHours(3), userid=id };
        try
        {
            _context.users.Add(newUser);
            await _context.SaveChangesAsync();

            _context.refresh_tokens.Add(refresh_token);
            await _context.SaveChangesAsync();

            Response.Cookies.Append("refreshtoken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            var accessToken = AccessToken.GenerateAccessToken(id.ToString() ?? "");
            Response.Cookies.Append("accesstoken", accessToken.ToString(), new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(15)
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.InnerException?.Message);
            Console.WriteLine(e);
            return StatusCode(500, new {error=e.Message, duplicateUsername = e.InnerException?.Message.Contains("users_username_key"), duplicateEmail = e.InnerException?.Message.Contains("users_email_key") });
        }
        
        return CreatedAtAction(nameof(GetUsers), new { }, user);
    }

    public CreateAccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.users.ToListAsync();
        return Ok(users);
    }
}