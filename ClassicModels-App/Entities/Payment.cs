namespace ClassicModels.Entities
{
    public class Payment
    {
        public int CustomerNumber { get; set; }
        public string CheckNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }

        public Customer Customer { get; set; }
    }
}
