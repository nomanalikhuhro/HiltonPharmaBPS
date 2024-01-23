namespace PASSForm_BPS.Models
{
    public class wf_workflow
    {
        public int WorkfFowId { get; set; }
        public string? WorkFlowName { get; set; }
        public int? Createdby { get; set; }
        public DateTime? CratedOn { get; set; }
        public Boolean? IsActive { get; set; }
        public int? Updateby { get; set; }
        public DateTime? Updatedon { get; set; }
    }
}
