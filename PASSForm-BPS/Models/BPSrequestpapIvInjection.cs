namespace PASSForm_BPS.Models
{
    public class BPSrequestpapIvInjection
    {
        public int BPS_Record_ID { get; set; }
        public int? HCPREQID { get; set; }
        public string? TrackingId { get; set; }
        public DateTime? DiscountFromDate { get; set; }
        public DateTime? DiscountToDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public int? StatusID { get; set; }
        public int? TypeId  { get; set; }

    }
}
