using Temporalio.Common;
using Temporalio.Workflows;

namespace Worker.Workflows;

[Workflow]
public class MainWorkflow
{
    // private readonly EmployeeActivities _employeeActivities;

    public MainWorkflow()
    {
        // _employeeActivities = new EmployeeActivities();
    }

    [WorkflowRun]
    public async Task<Guid> VerifyEmployee(Guid governmentDirectoryId)
    {
        var retryPolicy = new RetryPolicy
        {
            InitialInterval = TimeSpan.FromSeconds(1),
            MaximumInterval = TimeSpan.FromSeconds(100),
            BackoffCoefficient = 2,
            MaximumAttempts = 500,
        };

        try
        {
            var result = await Workflow.ExecuteActivityAsync(
                (EmployeeActivities employeeActivities)
                => employeeActivities.GetSocialSecurityNumber(governmentDirectoryId),
                new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5), RetryPolicy = retryPolicy }
            );
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Guid.Empty;
        }

    }
}
