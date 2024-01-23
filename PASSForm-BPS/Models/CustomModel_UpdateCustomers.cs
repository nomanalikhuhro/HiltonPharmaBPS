namespace PASSForm_BPS.Models
{
    public class CustomModel_UpdateCustomers
    {
        public string? ChemistCode { get; set; }
        public List<CustomModel_UpdateProducts>? ProductArr { get; set; }
        public string? posttoDate { get; set; }
        public string? postfromDate { get; set; }
        public string? grandtotal { get; set; }
        public string? bpspercentage { get; set; }
        public string? totalroipercentage { get; set; }
    }
}
