using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/user")]
public class GetUserController : ControllerBase
{
    private readonly AppDbContext _context;

    [HttpGet, Authorize]
    public async Task<IActionResult> GetUser()
    {
        var id = User.Identity?.Name;
        try
        {
            var user = await _context.users.Select(u => new GetUser
            {
                id = u.id,
                username = u.username,
                email = u.email
            }).Where(u => u.id.ToString() == id).FirstAsync();
            return Ok(new { user });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.InnerException });
        }
    }

    public GetUserController(AppDbContext context)
    {
        _context = context;
    }
}