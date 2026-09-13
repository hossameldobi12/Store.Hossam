using Shared.Dto.OrdersDto.Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Domain.Models.OrderModels
    {
        public class OrderResultDto 
        {
           
         

            public Guid Id { get; set; }
            public string UserEmail { get; set; }
            public ShippingAddressDto Address { get; set; }

            public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

            public string DeliveryMethod { get; set; }
           

            public string paymentStatus { get; set; } 
            public decimal Subtotal { get; set; }
            public DateTimeOffset dateTime { get; set; } = DateTimeOffset.Now;

            public string PaymentIntentId { get; set; } = string.Empty;

            public Decimal Total { get; set; }
        }

    }


