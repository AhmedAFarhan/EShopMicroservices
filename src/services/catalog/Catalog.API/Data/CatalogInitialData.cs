using Marten.Schema;

namespace Catalog.API.Data
{
	public class CatalogInitialData : IInitialData
	{
		public async Task Populate(IDocumentStore store, CancellationToken cancellation)
		{
			using var session = store.LightweightSession();

			if (await session.Query<Product>().AnyAsync())
				return;

			// Marten UPSERT
			session.Store<Product>(GetPreconfiguredProducts());

			await session.SaveChangesAsync();
		}

		public static IEnumerable<Product> GetPreconfiguredProducts() 
		{
			return new List<Product>()
			{
				new Product()
				{
					id = new Guid("2D85B8B8-7C2B-4530-A406-5021CC4D024A"),
					Name = "Name",
					Descreption = "Description",
					ImageFile = "",
					Price = 450.00M,
					Categories = new List<string>() {"Smart Phone"}
				}
			};
		}
	}


}
