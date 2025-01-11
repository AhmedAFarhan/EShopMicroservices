using FluentValidation;

namespace Catalog.API.Features.Products.CreateProduct
{
	public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
	public record CreateProductResult(Guid Id);

	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
        public CreateProductCommandValidator()
        {
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
			RuleFor(x => x.Categories).NotEmpty().WithMessage("Categories are required");
			RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be grater than zero");
		}
    }

	public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
	{
		public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
		{
			/*Business logic to create object*/
			//create product entity
			var product = new Product
			{
				Name = command.Name,
				Categories = command.Categories,
				Descreption = command.Description,
				ImageFile = command.ImageFile,
				Price = command.Price,
			};

			//save to database
			session.Store(product);
			await session.SaveChangesAsync(cancellationToken);

			//return result
			return new CreateProductResult(product.id);

		}
	}
}
