namespace PASSForm_BPS.Models
{
    public class tbl_doctorshospital_footfall
    {
        
        public int id { get; set; }
        public string? hospital_code { get; set; }

        public string? hospital_name { get; set; }

        public string? footfall { get; set; }
        public int hcp_reqid { get; set; }
    }
}
