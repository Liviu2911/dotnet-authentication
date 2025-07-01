using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/logout")]
public class LogoutController : ControllerBase
{
    private readonly AppDbContext _context;
    [HttpDelete]
    public async Task<IActionResult> Logout()
    {
        try
        {
            var token = Request.Cookies.Where(c => c.Key == "refreshtoken").First().Value;
            var refreshtoken = await _context.refresh_tokens.FirstOrDefaultAsync(rt => rt.token == token);

            _context.refresh_tokens.Remove(refreshtoken);
            await _context.SaveChangesAsync();

            Response.Cookies.Delete("refreshtoken");
            Response.Cookies.Delete("accesstoken");

            return Ok( new {mes = "User logged out"});
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.InnerException });
        }
    }

    public LogoutController(AppDbContext context)
    {
        _context = context;
    } 
}