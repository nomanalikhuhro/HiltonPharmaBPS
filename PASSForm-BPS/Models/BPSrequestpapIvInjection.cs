namespace PASSForm_BPS.Models
{
    public class BPSrequestpapIvInjection
    {
        public int BpsRecordId { get; set; }
        public int? Hcpreqid { get; set; }
        public string? TrackingId { get; set; }
        public DateTime? DiscountFromDate { get; set; }
        public DateTime? DiscountToDate { get; set; }
        public DateOnly? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateOnly? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
