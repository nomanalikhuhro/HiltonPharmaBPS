namespace PASSForm_BPS.Models
{
    public class wf_instance
    {
        public int WFInstanceId { get; set; }

        public int? WorkflowId { get; set; }

        public int? HcpReqId { get; set; }

        public string? TrackingID { get; set; }
        public int? Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? BpsId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
