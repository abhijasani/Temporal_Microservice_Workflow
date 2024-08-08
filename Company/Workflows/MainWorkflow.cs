using Temporalio.Common;
using Temporalio.Workflows;

namespace Company.Workflows;

[Workflow]
public class MainWorkflow
{
    [WorkflowRun]
    public async void VerifyEmployee()
    {
        var retryPolicy = new RetryPolicy
        {
            InitialInterval = TimeSpan.FromSeconds(1),
            MaximumInterval = TimeSpan.FromSeconds(100),
            BackoffCoefficient = 2,
            MaximumAttempts = 500,
        };

        // var governmentDirectoryId = Workflow.ExecuteActivityAsync(() => {
        //     EmployeeActivities
        // })
    }
}
