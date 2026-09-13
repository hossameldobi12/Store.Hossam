using Domain.Models.OrderModels;
using Shared.Dto.OrdersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderServices
    {
        public Task<OrderResultDto> GetOrderByIdAsync(Guid id);
        public Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail);
        public Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderDto,string userEmail);

        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();



    }
}
