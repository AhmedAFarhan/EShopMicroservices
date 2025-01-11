using BuildingBlocks.Behaviors;

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

var app = builder.Build();

//Configure the HTTP request pipeline

app.MapCarter();

app.Run();
