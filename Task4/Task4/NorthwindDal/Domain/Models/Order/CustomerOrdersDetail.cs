namespace NorthwindDal.Domain.Models.Order
{
    public class CustomerOrdersDetail
    {
        public string ProductName { get; set; }

        public float UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int Discount { get; set; }

        public decimal ExtendedPrice { get; set; }
    }
}