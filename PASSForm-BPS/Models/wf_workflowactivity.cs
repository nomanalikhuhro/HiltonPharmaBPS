namespace PASSForm_BPS.Models
{
    public class wf_workflowactivity
    {
        public int WorkflowActivityId { get; set; }
        public int? WorkflowId { get; set; }
        public string? WorkflowActivityName { get; set; }
        public string? ActionRule { get; set; }
        public int? Craetedby { get; set; }
        public DateTime? Createdon { get; set; }
    }
}
