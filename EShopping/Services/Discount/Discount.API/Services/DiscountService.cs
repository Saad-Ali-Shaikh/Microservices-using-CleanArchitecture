using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator mediator;

        public DiscountService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery(request.ProductName);
            var result = await mediator.Send(query);
            return result;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var command = new CreateDiscountCommand();
            command.ProductName = request.Coupon.ProductName;
            command.Description = request.Coupon.Description;
            command.Amount = request.Coupon.Amount;

            var result = await mediator.Send(command);
            return result;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = new UpdateDiscountCommand();
            command.Id = request.Coupon.Id;
            command.ProductName = request.Coupon.ProductName;
            command.Description = request.Coupon.Description;
            command.Amount = request.Coupon.Amount;

            var result = await mediator.Send(command);
            return result;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var command = new DeleteDiscountCommand(request.ProductName);
            var deleted = await mediator.Send(command);
            return new DeleteDiscountResponse { Success = deleted };
        }
    }
}
