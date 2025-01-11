
namespace Basket.API.Features.Basket.CreateBasket
{
	public record CreateBasketCommand(ShoppingCart Cart) : ICommand<CreateBasketResult>;
	public record CreateBasketResult(string UserName);

	public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
	{
		public CreateBasketCommandValidator()
        {
			RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
			RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is reqired");
		}
    }

	public class CreateBasketCommandHandler() : ICommandHandler<CreateBasketCommand, CreateBasketResult>
	{
		public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
		{
			ShoppingCart cart = command.Cart;

			return new CreateBasketResult("Ahmed");
		}
	}
}
