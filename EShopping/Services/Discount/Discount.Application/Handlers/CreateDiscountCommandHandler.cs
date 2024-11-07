using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handlers
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly IMapper mapper;
        public IDiscountRepository discountRepository { get; }

        public CreateDiscountCommandHandler(IDiscountRepository discountRepository,IMapper mapper)
        {
            this.discountRepository = discountRepository;
            this.mapper = mapper;
        }


        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = mapper.Map<Coupon>(request);
            await discountRepository.CreateDiscount(coupon);
            var couponModel = mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
