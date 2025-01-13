using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Discount.GRPC.Protos;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services
{
	public class DiscountService(DiscountDataContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
	{
		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var cuopon = await dbContext.Cuopons.FirstOrDefaultAsync(c =>c.ProductName == request.ProductName);
			if(cuopon is null)
			{
				cuopon = new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No discount description" };
			}

			var cuoponModel = cuopon.Adapt<CouponModel>();

			logger.LogInformation("Discount is retrieved for productname : {productname}", cuoponModel.ProductName);

			return cuoponModel;
		}

		public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			if(request.Coupon is null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Inavlid request object"));
			}

			var cuopon = request.Coupon.Adapt<Coupon>();

			await dbContext.Cuopons.AddAsync(cuopon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount coupon is created successfully");

			var couponModel = cuopon.Adapt<CouponModel>();

			return couponModel;
		}

		public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			if (request.Coupon is null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Inavlid request object"));
			}

			var cuopon = request.Coupon.Adapt<Coupon>();

			dbContext.Cuopons.Update(cuopon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount coupon is updated successfully");

			var couponModel = cuopon.Adapt<CouponModel>();

			return couponModel;

		}

		public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var cuopon = await dbContext.Cuopons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

			if (cuopon is null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, "object not found"));
			}

			dbContext.Remove(cuopon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount coupon is deleted successfully");

			return new DeleteDiscountResponse { IsSuccess = true };
		}
	}
}
