using AutoMapper;
using Domain.Models;
using Domain.Models.OrderModels;
using Services.Abstractions;
using Shared.Dto;
using Shared.Dto.OrdersDto;
using Shared.Dto.OrdersDto.Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderProfille : Profile
    {
        public OrderProfille()
        {
            CreateMap<Order, OrderResultDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.paymentStatus, o => o.MapFrom(s => s.paymentStatus.ToString()))
                .ForMember(d => d.Total, O => O.MapFrom(s => s.Subtotal + s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.productId, O => O.MapFrom(s => s.productInOrderItem.productId))
                .ForMember(D => D.ProductName, O => O.MapFrom(s => s.productInOrderItem.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom(s => s.productInOrderItem.PictureUrl));

            CreateMap<DeliveryMethod, DeliveryMethodDto>();

            CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();


        }
    }
}
