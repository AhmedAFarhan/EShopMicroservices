using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Data;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container

var assemply = typeof(Program).Assembly;

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assemply);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assemply);

builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
	builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

//Configure HTTP request pipline
app.MapCarter();

app.UseExceptionHandler(options => { });

//Check APIs are reachable and and database connection is not failed, and if there are another custom health check
app.UseHealthChecks("/health", new HealthCheckOptions()
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
	