using BackgroundCheck.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundCheck.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BackgroundCheckController : ControllerBase
{
    private readonly BackgroundCheckService _backgroundCheckService;

    public BackgroundCheckController(BackgroundCheckService backgroundCheckService)
    {
        _backgroundCheckService = backgroundCheckService;
    }

    [HttpGet]
    public IActionResult Verify(Guid SocialSecurityNumber)
    {
        var result = _backgroundCheckService.VerifyEmployee(SocialSecurityNumber);

        if(result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
