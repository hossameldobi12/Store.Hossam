namespace Domain.Models.OrderModels
{
    public class OrderItem :BaseEntity<Guid>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItem productInOrderItem, decimal price, int quantity)
        {
            this.productInOrderItem = productInOrderItem;
            Price = price;
            Quantity = quantity;
        }

        public ProductInOrderItem productInOrderItem { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}