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

    [HttpGet(nameof(GetTrafficViolation))]
    public IActionResult GetTrafficViolation(Guid SocialSecurityNumber)
    {
        var result = _backgroundCheckService.GetTrafficViolation(SocialSecurityNumber);

        if(result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet(nameof(GetCivilOffence))]
    public IActionResult GetCivilOffence(Guid SocialSecurityNumber)
    {
        var result = _backgroundCheckService.GetCivilOffences(SocialSecurityNumber);

        if(result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet(nameof(GetCriminalRecord))]
    public IActionResult GetCriminalRecord(Guid SocialSecurityNumber)
    {
        var result = _backgroundCheckService.GetCriminalRecord(SocialSecurityNumber);

        if(result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

}
