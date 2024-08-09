using Temporalio.Activities;
using Worker.Services;

namespace Worker.Workflows;

public class EmployeeActivities
{
    private readonly WebApiService _webApiService;

    public EmployeeActivities()
    {
        _webApiService = new WebApiService();        
    }

    [Activity]
    public async Task<Guid> GetGovtEmployeeId(Guid employeeId)
    {
        return await _webApiService.GetGovernmentEmployeeId(employeeId);
    }

    [Activity]
    public async Task<Guid> GetSocialSecurityNumber(Guid governmentDirectoryId)
    {
        return await _webApiService.GetSSN(governmentDirectoryId);
    }

    [Activity]
    public async Task<string> StartBackgroundCheck(Guid SocialSecurityNumber)
    {
        return await _webApiService.StartBackgroundCheck(SocialSecurityNumber);
    }
    
    [Activity]
    public async Task<bool> GetTrafficViolation(Guid SocialSecurityNumber)
    {
        return await _webApiService.GetTrafficViolation(SocialSecurityNumber);
    }

    [Activity]
    public async Task<bool> GetCivilOffence(Guid SocialSecurityNumber)
    {
        return await _webApiService.GetCivilOffence(SocialSecurityNumber);
    }

    [Activity]
    public async Task<bool> GetCriminalRecord(Guid SocialSecurityNumber)
    {
        return await _webApiService.GetCriminalRecord(SocialSecurityNumber);
    }

}
