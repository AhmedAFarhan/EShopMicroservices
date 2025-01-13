
using Basket.API.Data;
using Discount.GRPC.Protos;

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

	public class CreateBasketCommandHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountService) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
	{
		public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
		{
			foreach(var product in command.Cart.Items)
			{
				var coupon = await discountService.GetDiscountAsync(new GetDiscountRequest { ProductName = product.ProductName}, cancellationToken:cancellationToken);
				product.Price -= coupon.Amount; 
			}

			var basket = await basketRepository.CreateBasket(command.Cart, cancellationToken);

			return new CreateBasketResult(basket.UserName);
		}
	}

}
