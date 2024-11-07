using Catalog.Application.Commands;
using Catalog.Core.Reporsitories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        public DeleteProductByIdCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IProductRepository _productRepository { get; }

        public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.DeleteProduct(request.Id);
        }
    }
}
