namespace PASSForm_BPS.Models
{
    public class CustomModel_UpdateProducts
    {
        public string? ProductName { get; set; }
        public string? productCode { get; set; }
        public string? SalesType { get; set; }
        public string? pretotal { get; set; }
        public string? posttotal { get; set; }
        public string? postproductDescription { get; set; }
        public string? discountpercentage { get; set; }
        public string? discountpostcentage { get; set; }
        public List<CustomModel_UpdateSales>? Sale { get; set; }
    }
}
