using Temporalio.Common;
using Temporalio.Workflows;

namespace Worker.Workflows;

[Workflow]
public class MainWorkflow
{
    public MainWorkflow()
    {
    }

    [WorkflowRun]
    public async Task<string> VerifyEmployee(Guid governmentDirectoryId)
    {
        var retryPolicy = new RetryPolicy
        {
            InitialInterval = TimeSpan.FromSeconds(1),
            MaximumInterval = TimeSpan.FromSeconds(20),
            BackoffCoefficient = 2,
            MaximumAttempts = 500,
        };

        Guid ssnResult;

        ssnResult = await Workflow.ExecuteActivityAsync(
            (EmployeeActivities employeeActivities)
            => employeeActivities.GetSocialSecurityNumber(governmentDirectoryId),
            new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5), RetryPolicy = retryPolicy }
        );

        if (ssnResult == Guid.Empty)
        {
            return "SSN not found in GovtDirectory";
        }

        var checkResult = await Workflow.ExecuteActivityAsync(
            (EmployeeActivities employeeActivities)
            => employeeActivities.StartBackgroundCheck(ssnResult),
            new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5), RetryPolicy = retryPolicy }
        );

        if(checkResult == string.Empty)
        {
            return "SSN not found in BackgroundCheck";
        }

        return checkResult;
    }
}
