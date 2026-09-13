namespace Domain.Models.OrderModels
{
    public class ProductInOrderItem
    {
        public ProductInOrderItem()
        {
            
        }

        public ProductInOrderItem(int productId, string productName, string pictureUrl)
        {
            this.productId = productId;
            this.ProductName = productName;
            this.PictureUrl = pictureUrl;
        }
        public int productId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}