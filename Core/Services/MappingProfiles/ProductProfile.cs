using AutoMapper;
using Domain.Models;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                 .ForMember(p => p.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
                 .ForMember(p => p.ProductType, o => o.MapFrom(p => p.ProductType.Name))
                // .ForMember(p => p.PictureUrl, o => o.MapFrom(p => $"http://localhost:5298/{p.PictureUrl}"));
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductValueResolver>());
            CreateMap<ProductBrand, BrandDto>().ReverseMap()
                .ForMember(p => p.Name, o => o.MapFrom(p => p.Name));
            CreateMap<ProductType,TypeDto>().ReverseMap()
                .ForMember(p=>p.Name,o=>o.MapFrom(p => p.Name));
        }
    }
}
//images/products/ItalianChickenMarinade.png