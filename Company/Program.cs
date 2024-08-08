using Company.Services;
using Company.Workflows;
using Temporalio.Client;
using Temporalio.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<EmployeeService>();

var app = builder.Build();

// Create a client to connect to localhost on "default" namespace
var client = await TemporalClient.ConnectAsync(new("temporal:7233"));

// Cancellation token to shutdown worker on ctrl+c
using var tokenSource = new CancellationTokenSource();
Console.CancelKeyPress += (_, eventArgs) =>
{
    tokenSource.Cancel();
    eventArgs.Cancel = true;
};

var activities = app.Services.GetRequiredService<EmployeeActivities>();

// Create a worker with the activity and workflow registered
using var worker = new TemporalWorker(
    client, // client
    new TemporalWorkerOptions(taskQueue: "EMPLOYEE_VERIFICATION_TASK_QUEUE")
        .AddAllActivities(activities) // Register activities
        .AddWorkflow<MainWorkflow>() // Register workflow
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();