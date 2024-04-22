namespace PASSForm_BPS.ViewModel
{
    public class BPSPharmaciesPAPSalesViewModel
    {


        public int? ChemistCode { get; set; }

        public int? PackCode { get; set; }
        public string? ProductName { get; set; }

        public decimal? LastYearSKU { get; set; }

        public decimal? LastYearValue { get; set; }
        public decimal? UnitPrice { get; set; }
     

        public decimal? ExpectedBusinessUnit { get; set; }

        public decimal? ExpectedBusinessValue { get; set; }
        public decimal? Discount { get; set; }

    }
}
