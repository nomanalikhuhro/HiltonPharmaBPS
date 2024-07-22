using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace PASSForm_BPS.Models
{
    public class TransactionDetails
    {
        
        public int? Id { get; set; }
        public string? InvoiceNumber { get; set; }  
        public DateTime? InvoiceDate { get; set; }  
        //public string? ProductCode { get; set; }  
        public int? OrderID { get; set; } 
        public int? Quantity { get; set; } 
        public int? Createdby { get; set; } 
        public DateTime? Createdon { get; set; } 
        public int? Updatedby { get; set; }
        public DateTime? Updatedon { get; set; }
        public string? OrderNo { get; set; } 
        //public string? PackCode { get; set; } 
        public int? TransactionId { get; set; }
       

    }
}
