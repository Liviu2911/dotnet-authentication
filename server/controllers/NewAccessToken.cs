using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/newtoken")]
public class NewAccessTokenController : ControllerBase
{
    private readonly AppDbContext _context;

    [HttpGet]
    public async Task<IActionResult> NewAccessToken()
    {
        var hasToken = Request.Cookies.ContainsKey("refreshtoken");
        if (!hasToken)
        {
            return StatusCode(500, new { error = "logout", mes = "no refresh toekn" });
        }
        var refreshtoken = Request.Cookies.Where(c => c.Key == "refreshtoken").First().Value;
        if (refreshtoken == null)
        {
            return StatusCode(500, new { error = "logout" });
        }
        try
        {
            var refresh_token = await _context.refresh_tokens.Where(r => r.token == refreshtoken).FirstAsync();
            if (refresh_token == null || refresh_token.id < 1)
            {
                return StatusCode(500, new { error = "logout", mes = "Refresh token not found" });
            }

            string userid = refresh_token.userid.ToString() ?? "";
            var newAccessToken = AccessToken.GenerateAccessToken(userid);
            Response.Cookies.Append("accesstoken", newAccessToken, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });
            return Ok(new { mes = "Access token created" });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.InnerException, mes = "no gud" });
        }
    }

    public NewAccessTokenController(AppDbContext context)
    {
        _context = context;
    }
}