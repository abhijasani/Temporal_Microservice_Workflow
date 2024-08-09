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
    public async Task<Guid> GetSocialSecurityNumber(Guid governmentDirectoryId)
    {
        return await _webApiService.GetSSN(governmentDirectoryId);
    }

    [Activity]
    public Guid DummyActivity()
    {
        return Guid.NewGuid();
    }
}
