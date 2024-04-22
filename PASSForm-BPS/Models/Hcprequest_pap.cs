using System.ComponentModel.DataAnnotations;

namespace PASSForm_BPS.Models
{
    public class Hcprequest_pap
    {


        public int? HCPREQID { get; set; }


        public string? TrackingID { get; set; }
        public string? ASMCode { get; set; }


        public string? TMCode { get; set; }
        public string? TMEmpNo { get; set; }
        public int? HCPID { get; set; }
        public int? HospID { get; set; }
        public string? DescriptionOfPlan { get; set; }
        public int? Status_ID { get; set; }
        public string? PendingTo { get; set; }
        public string? Comments { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? TeamId { get; set; }
        public string? Basearea { get; set; }
    }
}
