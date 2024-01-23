

namespace PASSForm_BPS.Models
{
    public class wf_comments
    {
        public int ID { get; set; }
        public int WFWorklistId { get; set; }

        public string? Comments { get; set; }
        public string? UploadFilePath { get; set; }
        public DateTime? Createdon { get; set; }
        public int? Createdby { get; set; }
        public string? TrackingId { get; set; }
    }
}
