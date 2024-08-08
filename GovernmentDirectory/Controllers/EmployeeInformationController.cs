using GovernmentDirectory.Models;
using GovernmentDirectory.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GovernmentDirectory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeInformationController : ControllerBase
{
    private EmployeeInformationService _employeeInformationService;
    
    public List<EmployeeInformation> EmployeeInformationList = [];

    public EmployeeInformationController(EmployeeInformationService employeeInformationService)
    {
        _employeeInformationService = employeeInformationService;
    }
    
    [HttpGet]
    public IActionResult GetSocialSecurityNumber(Guid GovernmentDirectoryId)
    {
        var id = _employeeInformationService.GetEmployeeInformation(GovernmentDirectoryId);

        if(id == Guid.Empty)
        {
            return NotFound();
        }

        return Ok(id);
    }

}
