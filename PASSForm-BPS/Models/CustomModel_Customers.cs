namespace PASSForm_BPS.Models
{
    public class CustomModel_Customers
    {
        public string? ChemistCode { get; set; }
        public List<CustomModel_Product>? ProductArr { get; set; }
        public string? posttoDate { get; set; }
        public string? postfromDate { get; set; }
        public string? pretoDate { get; set; }
        public string? prefromDate { get; set; }
        public string? grandtotal { get; set; }
        public string? bpspercentage { get; set; }
        public string? totalroipercentage { get; set; }



    }
}
