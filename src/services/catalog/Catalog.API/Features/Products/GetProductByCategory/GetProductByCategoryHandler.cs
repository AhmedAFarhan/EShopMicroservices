using System.Linq;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Features.Products.GetProductByCategory
{
	public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
	public record GetProductByCategoryResult(IEnumerable<Product> Products);

	public class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
	{
		public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
		{
			var products = await session.Query<Product>().Where(p=>p.Categories.Contains(query.category)).ToListAsync(cancellationToken);

			return new GetProductByCategoryResult(products);
		}
	}
}
