
using Basket.API.Exceptions;
using Marten;

namespace Basket.API.Data
{
	public class BasketRepository(IDocumentSession session) : IBasketRepository
	{
		public async Task<ShoppingCart> CreateBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
		{
			session.Store(shoppingCart);
			await session.SaveChangesAsync();
			return shoppingCart;
		}

		public async Task<bool> DeleteBasket(string Username, CancellationToken cancellationToken = default)
		{
			session.Delete<ShoppingCart>(Username);
			await session.SaveChangesAsync();
			return true;
		}

		public async Task<ShoppingCart> GetBasket(string Username, CancellationToken cancellationToken = default)
		{
			var basket = await session.LoadAsync<ShoppingCart>(Username, cancellationToken);

			if(basket is null)
			{
				throw new BasketNotFoundException(Username);
			}

			return basket;
		}
	}
}
