using Shared.Dto;
using Shared.SpecificationsParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductAsync(ProductSpecsParam productSpecsParam);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
