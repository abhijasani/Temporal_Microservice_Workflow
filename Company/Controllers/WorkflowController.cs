using Microsoft.AspNetCore.Mvc;
using Temporalio.Client;
using Worker.Workflows;

namespace Company.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkflowController : ControllerBase
{
    public WorkflowController()
    {
    }

    [HttpGet]
    public async Task StartEmployeeVerificationWorkflowAsync(Guid governmentEmployeeId)
    {
        var client = await TemporalClient.ConnectAsync(new("temporal:7233") { Namespace = "default" });

        var workflowId = $"employee-verification-{Guid.NewGuid()}";

        try
        {
            // Start the workflow
            var handle = await client.StartWorkflowAsync(
                (MainWorkflow wf) => wf.VerifyEmployee(governmentEmployeeId),
                new(id: workflowId, taskQueue: "EMPLOYEE_VERIFICATION_TASK_QUEUE"));

            Console.WriteLine($"Started Workflow {workflowId}");

            // Await the result of the workflow
            var result = await handle.GetResultAsync<Guid>();
            Console.WriteLine($"Workflow result: {result}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Workflow execution failed: {ex.Message}");
        }
    }
}
