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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IProductRepository _productRepository { get; }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _productRepository.UpdateProduct(new Core.Entities.Product
            {
                Id = request.Id,
                Name = request.Name,
                ImageFile = request.ImageFile,
                Price = request.Price,
                Brands = request.Brands,
                Description = request.Description,
                Summary = request.Summary,
                Types = request.Types,
            });

            return true;
        }
    }
}
