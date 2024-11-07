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
    public class GetAllProductTypesHandler : IRequestHandler<GetAllProductTypesQuery, IList<ProductTypeResponse>>
    {
        private ITypesRepository _typesRepository;
        public GetAllProductTypesHandler(ITypesRepository typesRepository)
        {
            _typesRepository = typesRepository;
        }
        public async Task<IList<ProductTypeResponse>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypes = await _typesRepository.GetAllTypes();
            var productTypeResponse = ProductMapper.Mapper.Map<IList<ProductTypeResponse>>(productTypes);
            return productTypeResponse;
        }
    }
}
