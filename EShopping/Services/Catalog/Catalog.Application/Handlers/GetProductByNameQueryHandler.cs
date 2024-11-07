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
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
    {
        public GetProductByNameQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IProductRepository _productRepository { get; }

        public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByName(request.Name);
            var productResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(product);
            return productResponse;
        }
    }
}
