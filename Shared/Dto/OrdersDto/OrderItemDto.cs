namespace Shared.Dto.OrdersDto.Domain.Models.OrderModels
{
    public class OrderItemDto
    {
        public int productId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}