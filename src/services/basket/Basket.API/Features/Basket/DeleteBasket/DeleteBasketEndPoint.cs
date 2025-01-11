
using Basket.API.Features.Basket.GetBasket;

namespace Basket.API.Features.Basket.DeleteBasket
{
	public record DeleteBasketRequest(string Username);
	public record DeleteBasketResponse(bool IsSuccess);

	public class DeleteBasketEndPoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/baskets/{userName}", async (string username ,ISender sender) =>
			{
				var result = sender.Send(new DeleteBasketCommand(username));

				var response = result.Adapt<DeleteBasketResponse>();

				return Results.Ok(response);
			})
			.WithName("DeleteBasket")
			.Produces<GetBasketResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Delete Basket")
			.WithDescription("Delete Basket"); ;
		}
	}
}
