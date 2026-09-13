using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions.NotFound;
using Domain.Models;
using Services.Abstractions;
using Services.Specifications;
using Shared.Dto;
using Shared.SpecificationsParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDto>> GetAllProductAsync(ProductSpecsParam productSpecsParam)
        {
            var productspecifactions = new ProductSpecifications(productSpecsParam);
            var products = await _unitOfWork.GetReposiotry<Product, int>().GetAllAsync(productspecifactions);
            var result = mapper.Map<IEnumerable<ProductDto>>(products);
            return result;

        }
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var productspecifactions = new ProductSpecifications(id);
            var product = await _unitOfWork.GetReposiotry<Product, int>().GetByIdAsync(productspecifactions);
            if (product is null) throw new ProductNotFoundException(id);
            var result = mapper.Map<ProductDto>(product);
            return result;
        }
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetReposiotry<ProductBrand, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<BrandDto>>(brands);
            return result;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetReposiotry<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeDto>>(types);
            return result;
        }


    }
}
