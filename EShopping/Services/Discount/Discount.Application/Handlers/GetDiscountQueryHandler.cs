using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository discountRepository;

        public GetDiscountQueryHandler(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.ProductName} not found."));
            }
            var couponModel = new CouponModel { ProductName = coupon.ProductName, Amount = coupon.Amount, Id = coupon.Id, Description = coupon.Description };

            return couponModel;
        }
    }
}
