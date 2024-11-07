using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository discountRepository;
        private readonly IMapper mapper;

        public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            this.discountRepository = discountRepository;
            this.mapper = mapper;
        }
        public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = mapper.Map<Coupon>(request);
            await discountRepository.UpdateDiscount(coupon);
            var couponModel = mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
