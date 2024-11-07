using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Reporsitories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IProductRepository _productRepository { get; }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<Product>(request);
            if (productEntity is null)
            {
                throw new ApplicationException("There is an issue with mapping table with creating the new product");
            }

            var newProduct = await _productRepository.CreateProduct(productEntity);
            return ProductMapper.Mapper.Map<ProductResponse>(newProduct);
        }
    }
}
