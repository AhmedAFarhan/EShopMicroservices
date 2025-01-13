using Basket.API.Data;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Discount.GRPC.Protos;

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
	options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
				.AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
				.AddRedis(builder.Configuration.GetConnectionString("Redis")!);

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
	options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
	var handler = new HttpClientHandler
	{
		ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
	};

	return handler;
});

var app = builder.Build();

//Configure the HTTP request pipeline

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
	new HealthCheckOptions
	{
		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
	});

app.Run();
