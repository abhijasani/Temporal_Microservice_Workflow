using Company.Services;
using Temporalio.Activities;

namespace Company.Workflows;

public class EmployeeActivities
{
    private readonly WebApiService _webApiService;

    public EmployeeActivities(WebApiService webApiService)
    {
        _webApiService = webApiService;        
    }

    [Activity]
    public async Task GetSocialSecurityNumber(Guid governmentDirectoryId)
    {
        var employeeSSN = await _webApiService.GetSSN(governmentDirectoryId);
    }
}
