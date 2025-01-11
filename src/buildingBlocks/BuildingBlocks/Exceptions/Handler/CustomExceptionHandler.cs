using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
	public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
		{
			logger.LogError("Error Message : {exceptionMessage}, Time of occurrence {time}", exception.Message, DateTime.Now);

			(string Detail, string Title, int StatusCode) details = exception switch
			{
				InternalServerException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status500InternalServerError),
				BadRequestException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status400BadRequest),
				ValidationException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status400BadRequest),
				NotFoundException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status404NotFound),
				_ => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status500InternalServerError),
			};

			var probleDetails = new ProblemDetails
			{
				Detail = details.Detail,
				Title = details.Title,
				Status = details.StatusCode,
				Instance = context.Request.Path
			};

			if(exception is ValidationException validationException)
			{
				probleDetails.Extensions.Add("ValidationErrors", validationException.Errors);
			}

			await context.Response.WriteAsJsonAsync(probleDetails, cancellationToken:cancellationToken);

			return true;
		}
	}
}
