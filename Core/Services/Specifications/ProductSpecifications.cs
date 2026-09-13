using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared.SpecificationsParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>, IproductSpecifications
    {

        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }


        public ProductSpecifications(ProductSpecsParam productSpecsParam) :
            base(
                 p => 
                     (string.IsNullOrEmpty(productSpecsParam.Search) || p.Name.ToLower().Contains(productSpecsParam.Search.ToLower()))
                  && (!productSpecsParam.BrandId.HasValue || p.BrandId == productSpecsParam.BrandId)
                  && (!productSpecsParam.TypeId.HasValue || p.TypeId == productSpecsParam.TypeId))

        {

            ApplyIncludes();
            AddSort(productSpecsParam.Sort);
            AddPagination(productSpecsParam.PageIndex, productSpecsParam.PageSize);

        }

        private void ApplyIncludes()
        {

            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }

        private void AddSort(string sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {

                    case "namedes":
                        AddOrderByDes(p => p.Name);
                        break;
                    case "price":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedes":
                        AddOrderByDes(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }

        }

    }
}
