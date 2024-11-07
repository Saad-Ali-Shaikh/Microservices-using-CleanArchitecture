using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class DeleteBasketByUserNameCommandHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
    {
        private readonly IBasketRepository basketRepository;

        public DeleteBasketByUserNameCommandHandler(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            await basketRepository.DeleteBasket(request.UserName);
            return Unit.Value;
        }
    }
}
