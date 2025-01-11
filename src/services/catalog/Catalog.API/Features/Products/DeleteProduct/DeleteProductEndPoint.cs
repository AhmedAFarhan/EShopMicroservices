
using Catalog.API.Features.Products.CreateProduct;

namespace Catalog.API.Features.Products.DeleteProduct
{
	//public record DeleteProductRequest();
	public record DeleteProductResponse(bool IsSuccess);

	public class DeleteProductEndPoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
			{
				var result = await sender.Send(new DeleteProductCommand(id));

				var response = result.Adapt<DeleteProductResult>();

				return Results.Ok(response);
			})
			.WithName("DeleteProduct")
			.Produces<DeleteProductResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Delete product")
			.WithDescription("Delete Product"); ;
		}
	}
}
