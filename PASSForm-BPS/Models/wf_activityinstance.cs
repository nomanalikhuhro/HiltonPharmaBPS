namespace PASSForm_BPS.Models
{
    public class wf_activityinstance
    {
        public int WFActivityInstanceId { get; set; }

        public int WFInstanceId { get; set; }

        public int? WorkflowActivityId { get; set; }

        public int? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
