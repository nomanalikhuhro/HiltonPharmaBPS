namespace PASSForm_BPS.Models
{
    public class tblproduct
    {
        public int Id { get; set; }

        public string? TeamCode { get; set; }
        public string? TeamName { get; set; }
        public string? PackCode { get; set; }
        public string? Description { get; set; }
        public string? ProductName { get; set; }
        public double? UnitPrice { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

    }
}
