namespace ClassicModels.Entities
{
    public class Product
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductLineName { get; set; }
        public string ProductScale { get; set; }
        public string ProductVendor { get; set; }
        public string ProductDescription { get; set; }
        public int QuantityInStock { get; set; }
        public double BuyPrice { get; set; }
        public double Msrp { get; set; }

        public ICollection<OrderDetail>? OrdersDetails { get; set; }
        public ProductLine ProductLineNavigation { get; set; }

    }
}
