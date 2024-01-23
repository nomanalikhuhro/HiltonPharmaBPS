namespace PASSForm_BPS.Models
{
    public class wf_uploadfilespath
    {
        public int ID { get; set; }
        public int WFWorklistId { get; set; }
        public string? UploadFilePath { get; set; }
        public string? FileName { get; set; }
    }
}
