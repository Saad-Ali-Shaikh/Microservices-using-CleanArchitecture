using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Reporsitories;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetAllProductsHander : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
    {
        public IProductRepository _productRepository;
        public GetAllProductsHander(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {

            var productList = await _productRepository.GetProducts(request.Params);
            var productResponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
            return productResponseList;
        }
    }
}
