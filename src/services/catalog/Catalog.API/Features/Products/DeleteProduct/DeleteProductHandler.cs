
using Catalog.API.Features.Products.UpdateProduct;
using FluentValidation;

namespace Catalog.API.Features.Products.DeleteProduct
{
	public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
	public record DeleteProductResult(bool IsSuccess);

	public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
	{
		public DeleteProductCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
		}
	}

	public class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
	{
		public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
		{
			var product = session.LoadAsync<Product>(command.Id, cancellationToken);

			session.Delete<Product>(command.Id);

			await session.SaveChangesAsync(cancellationToken);

			return new DeleteProductResult(true);
		}
	}
}
