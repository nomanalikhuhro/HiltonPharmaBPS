namespace PASSForm_BPS.Models
{
    public class Bpssalesrecordpap

    { 
    public int Record_ID { get; set; }

    public int? Bps_RecordID { get; set; }

    public int? ChemistCode { get; set; }

    public int? PackCode { get; set; }

    public decimal? LastYearSKU { get; set; }

    public decimal? LastYearValue { get; set; }

    public int? Year { get; set; }

    public decimal? Discount { get; set; }

    public decimal? ExpectedBusinessUnit { get; set; }

    public decimal? ExpectedBusinessValue { get; set; }

    public decimal? UnitPrice { get; set; }

    public DateTime? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdateOn { get; set; }
    public decimal? Capping { get; set; }

    }
}
