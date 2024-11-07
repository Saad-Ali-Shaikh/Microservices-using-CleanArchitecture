using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetProductByBrandQuery : IRequest<IList<ProductResponse>>
    {
        public GetProductByBrandQuery(string brandName)
        {
            BrandName = brandName;
        }

        public string BrandName { get; }
    }
}
