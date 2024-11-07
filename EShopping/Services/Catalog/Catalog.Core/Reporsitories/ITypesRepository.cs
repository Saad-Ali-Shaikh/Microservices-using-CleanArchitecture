using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Reporsitories
{
    public interface ITypesRepository
    {
        Task<IEnumerable<ProductType>> GetAllTypes(); 
    }
}
