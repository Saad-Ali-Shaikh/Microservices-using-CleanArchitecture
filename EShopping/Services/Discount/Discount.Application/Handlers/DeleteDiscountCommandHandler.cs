using Discount.Application.Commands;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepository discountRepository;

        public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository;
        }
        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var deleted = await discountRepository.DeleteDiscount(request.ProductName);
            return deleted;
        }
    }
}
