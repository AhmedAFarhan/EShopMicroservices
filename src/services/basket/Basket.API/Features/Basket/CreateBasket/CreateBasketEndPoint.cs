
using Basket.API.Features.Basket.GetBasket;

namespace Basket.API.Features.Basket.CreateBasket
{
	public record CreateBasketRequest(ShoppingCart Cart);
	public record CreateBasketResponse(string UserName);

	public class CreateBasketEndPoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/baskets", async (CreateBasketRequest request, ISender sender) =>
			{
				var command = request.Adapt<CreateBasketCommand>();

				var result = await sender.Send(command);

				var response = result.Adapt<CreateBasketResponse>();

				return Results.Created($"/baskets/{response.UserName}", response);
			})
			.WithName("CreateBasket")
			.Produces<GetBasketResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Create Basket")
			.WithDescription("Create Basket"); 
		}
	}
}
