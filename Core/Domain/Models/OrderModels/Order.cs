using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order( string userEmail, ShippingAddress address, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            Address = address;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

       
        public string UserEmail { get; set; }
        public ShippingAddress Address { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }

        public OrderPaymentStatus paymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public decimal Subtotal { get; set; }
        public DateTimeOffset dateTime { get; set; } = DateTimeOffset.Now;

        public string PaymentIntentId { get; set; }
    }

}
