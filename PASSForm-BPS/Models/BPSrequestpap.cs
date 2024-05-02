using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations;

namespace PASSForm_BPS.Models
{
    public class BPSrequestpap


    {
        [ScaffoldColumn(false)] 
        
        public int BPS_Record_Id { get; set; }
        public int? HCPREQID { get; set; }
        public string? TrackingID { get; set; }


        public DateTime DiscountDateFrom { get; set; }
      

        public DateTime DiscountDateTo { get; set; }
        public string? DistributerCode { get; set; }
        public string? BrickCode { get; set; }
        public string? Remarks { get; set; }
        public string? DiscountType { get; set; }
        public int? TypeId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string? UpdatedBy { get; set; }

        public int? StatusID { get; set; }
        
    }
}
