namespace PASSForm_BPS.Models
{
    public class BpssalesrecordpapIvInjection
    {
        public int RecordId { get; set; }

        public int? BPS_Record_ID { get; set; }

        public string? Team { get; set; }

        public int? PackCode { get; set; }
        public decimal? Discount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
