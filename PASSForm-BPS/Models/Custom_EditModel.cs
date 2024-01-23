using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace PASSForm_BPS.Models
{
    public class Custom_EditModel
    {
        public decimal? ActualDiscount { get; set; }
        public decimal? Contribution { get; set; }
        public string? PackCode { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? SalesType { get; set; }
        public decimal? PreTotalSal { get; set; }
        public decimal? PostTotalSal { get; set; }
        public decimal? ROI { get; set; }

        [NotMapped] // Add this attribute to specify that this property is not mapped to the database
        public Dictionary<string, decimal>? SalesByMonthYear { get; set; }


    }
}
