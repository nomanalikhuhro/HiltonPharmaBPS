namespace PASSForm_BPS.Models
{
    public class CustomModel_Product
    {
        public string? ProductName { get; set; }
        public string? productCode { get; set; }
        public string? Contribution { get; set; }
        public string? SalesType { get; set; }
        public string? pretotal { get; set; }
        public string? posttotal { get; set; }
        public string? roi { get; set; }
        public string? postproductDescription { get; set; }
        public string? preproductActualDiscount { get; set; }
        public string? discountpercentage { get; set; }
        public string? discountpostcentage { get; set; }






        public List<CustomModel_Sales>? Sale { get; set; }
    }
}
