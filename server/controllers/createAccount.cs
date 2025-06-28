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
        var newUser = new User{id=id, username=user.username, email=user.email, password=hashedPassword};
        var token = Generator.GenerateRefreshToken();
        var refresh_token = new  RefreshToken { token = token, userid = id, created_at = DateTime.UtcNow, expires_at = DateTime.UtcNow.AddHours(3) };
        try
        {
            _context.users.Add(newUser);
            await _context.SaveChangesAsync();

            _context.refresh_tokens.Add(refresh_token);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.InnerException?.Message);
            return StatusCode(500, new { duplicateUsername = e.InnerException?.Message.Contains("duplicate key value violates unique constraint \"users_username_key\""), duplicateEmail = e.InnerException?.Message.Contains("duplicate key value violates unique constraint \"users_email_key\"") } );
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