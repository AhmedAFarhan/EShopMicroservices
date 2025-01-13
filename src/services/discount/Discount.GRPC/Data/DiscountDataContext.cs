using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data
{
	public class DiscountDataContext : DbContext
	{
        public DiscountDataContext(DbContextOptions<DiscountDataContext> options): base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var cupons = new List<Coupon>()
			{
				new Coupon(){Id = 1, ProductName = "IPhone X", Description="IPhone discount", Amount = 150},
				new Coupon(){Id = 2, ProductName = "Samsung 10", Description="Samsung discount", Amount = 300},
			};
			modelBuilder.Entity<Coupon>().HasData(cupons);
		}

		public DbSet<Coupon> Cuopons { get; set; }
    }
}
