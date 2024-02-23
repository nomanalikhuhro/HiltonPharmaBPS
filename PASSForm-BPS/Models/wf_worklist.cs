namespace PASSForm_BPS.Models
{
    public class wf_worklist
    {
        public int WFWorklistId { get; set; }
        public int? WFActivityInstanceId { get; set; }
        public string? Destination { get; set; }
        public string? Designation { get; set; }
        public string? Action { get; set; }
        public int? ActionBy { get; set; }
        public int? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? TrackingId { get; set; }

        public int? HcpReqId { get; set; }
        public int? BpsId { get; set; }
        public string? TMCode { get; set; }
        public string? Statustype { get; set; }
        public string? CreatedBy { get; set; }

        public string? ActionByName { get; set; }
        public string? ActivityStatus { get; set; }
        public string? User_Name { get; set; }
    }
}
