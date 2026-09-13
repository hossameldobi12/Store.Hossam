using Shared.Dto.OrdersDto.Domain.Models.OrderModels;

namespace Services.Abstractions
{
    public class OrderRequestDto
    {
        public string BasketId { get; set; }
        public ShippingAddressDto Address { get; set; }
        public int delveryMethodId { get; set; }
    }
}