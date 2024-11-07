using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Reporsitories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetProductByBrandQueryHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductResponse>>
    {
        public GetProductByBrandQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IProductRepository _productRepository { get; }

        public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByBrand(request.BrandName);
            var productResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(product);
            return productResponse;
        }
    }
}
