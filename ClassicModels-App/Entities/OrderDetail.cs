namespace ClassicModels.Entities
{
    public class OrderDetail
    {
        public int OrderNumber { get; set; }
        public string ProductCode { get; set; }
        public int QuantityOrdered { get; set; }
        public double PriceEach { get; set; }
        public int OrderLineNumber { get; set; }

        public Order Order { get; set; }
        public Product? Product { get; set; }
    }
}
