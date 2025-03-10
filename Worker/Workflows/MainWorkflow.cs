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
    public async Task<string> VerifyEmployee(Guid employeeId)
    {
        var retryPolicy = new RetryPolicy
        {
            InitialInterval = TimeSpan.FromSeconds(1),
            MaximumInterval = TimeSpan.FromSeconds(20),
            BackoffCoefficient = 2,
            MaximumAttempts = 500,
        };

        Guid govtIdResult;

        govtIdResult = await Workflow.ExecuteActivityAsync(
            (EmployeeActivities employeeActivities)
            => employeeActivities.GetGovtEmployeeId(employeeId),
            new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5), RetryPolicy = retryPolicy }
        );

        if (govtIdResult == Guid.Empty)
        {
            return "GovtID not found in CompanyDirectory";
        }

        Guid ssnResult;

        ssnResult = await Workflow.ExecuteActivityAsync(
            (EmployeeActivities employeeActivities)
            => employeeActivities.GetSocialSecurityNumber(govtIdResult),
            new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5), RetryPolicy = retryPolicy }
        );

        if (ssnResult == Guid.Empty)
        {
            return "SSN not found in GovtDirectory";
        }

        // var checkResult = await Workflow.ExecuteActivityAsync(
        //     (EmployeeActivities employeeActivities)
        //     => employeeActivities.StartBackgroundCheck(ssnResult),
        //     new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5), RetryPolicy = retryPolicy }
        // );

        // if(checkResult == string.Empty)
        // {
        //     return "SSN not found in BackgroundCheck";
        // }

        // return checkResult;

        var trafficViolationResult = Workflow.ExecuteChildWorkflowAsync(
        (TrafficViolationWorkflow w) => w.GetTrafficViolation(ssnResult),
            new ChildWorkflowOptions { Id = $"{nameof(TrafficViolationWorkflow)}-{ssnResult}" }
        );

        var civilOffenceResult = Workflow.ExecuteChildWorkflowAsync(
        (CivilOffenceWorkflow w) => w.GetCivilOffence(ssnResult),
            new ChildWorkflowOptions { Id = $"{nameof(CivilOffenceWorkflow)}-{ssnResult}" }
        );

        var criminalRecordResult = Workflow.ExecuteChildWorkflowAsync(
        (CriminalRecordWorkflow w) => w.GetCriminalRecord(ssnResult),
            new ChildWorkflowOptions { Id = $"{nameof(CriminalRecordWorkflow)}-{ssnResult}" }
        );

        await Task.WhenAll(trafficViolationResult, civilOffenceResult, criminalRecordResult);

        return $"{await trafficViolationResult}-{await civilOffenceResult}-{await criminalRecordResult}";
    }
}
