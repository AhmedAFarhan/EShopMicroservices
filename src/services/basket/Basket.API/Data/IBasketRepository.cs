namespace Basket.API.Data
{
	public interface IBasketRepository
	{
		Task<ShoppingCart> GetBasket(string Username, CancellationToken cancellationToken = default!);
		Task<ShoppingCart> CreateBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default!);
		Task<bool> DeleteBasket(string Username, CancellationToken cancellationToken = default!);
	}
}
