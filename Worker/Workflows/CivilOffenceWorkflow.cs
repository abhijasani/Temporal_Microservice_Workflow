using Temporalio.Common;
using Temporalio.Workflows;

namespace Worker.Workflows;

[Workflow]
public class CivilOffenceWorkflow
{
    [WorkflowRun]
    public async Task<bool> GetCivilOffence(Guid SSN)
    {
        var retryPolicy = new RetryPolicy
        {
            InitialInterval = TimeSpan.FromSeconds(1),
            MaximumInterval = TimeSpan.FromSeconds(20),
            BackoffCoefficient = 2,
            MaximumAttempts = 500,
        };


        var result = await Workflow.ExecuteActivityAsync(
            (EmployeeActivities employeeActivities)
            => employeeActivities.GetCivilOffence(SSN),
            new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5), RetryPolicy = retryPolicy }
        );

        return result;
    }
}
