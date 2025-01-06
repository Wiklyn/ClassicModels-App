namespace ClassicModels.Entities
{
    public class ProductLine
    {
        public string ProductLineName { get; set; }
        public string? TextDescription { get; set; }
        public string? HtmlDescription { get; set; }
        public byte[]? Image { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
