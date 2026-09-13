namespace Domain.Models.OrderModels
{
    public class ShippingAddress
    {
        public ShippingAddress()
        {
            
        }
        public ShippingAddress(string firstName, string lastName, string street, string city, string country)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.street = street;
            this.city = city;
            this.country = country;
        }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
}