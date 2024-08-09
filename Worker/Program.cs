// Create a client to connect to localhost on "default" namespace
using Temporalio.Client;
using Temporalio.Worker;
using Worker.Workflows;

var client = await TemporalClient.ConnectAsync(new("localhost:7233"));

// Cancellation token to shutdown worker on ctrl+c
using var tokenSource = new CancellationTokenSource();
Console.CancelKeyPress += (_, eventArgs) =>
{
    tokenSource.Cancel();
    eventArgs.Cancel = true;
};

var activities = new EmployeeActivities();

using var worker = new TemporalWorker(
    client, // client
    new TemporalWorkerOptions(taskQueue: "EMPLOYEE_VERIFICATION_TASK_QUEUE")
        .AddAllActivities(activities) // Register activities
        .AddWorkflow<MainWorkflow>() // Register workflow
        .AddWorkflow<TrafficViolationWorkflow>()
        .AddWorkflow<CivilOffenceWorkflow>()
        .AddWorkflow<CriminalRecordWorkflow>()
);

// Run the worker until it's cancelled
Console.WriteLine("Running worker...");
try
{
    await worker.ExecuteAsync(tokenSource.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Worker cancelled");
}