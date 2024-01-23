using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class BpsRequest
{
    public int BpsRecordId { get; set; }

    public int? Hcpreqid { get; set; }

    public string? MacroBrickCode { get; set; }
    public string? TMCode { get; set; }
    public string? User_Name { get; set; }
    public string? StatusType { get; set; }

    
        
        

    public string? DistributerCode { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? StatusId { get; set; }

    public int? ApproverOrder { get; set; }

    public int? CurrentApproverEmpId { get; set; }

    public string? TrackingId { get; set; }
    public string? PONumber { get; set; }
    public string? ActivityStatus { get; set; }
    public string? Comments { get; set; }




    public virtual Distributer? DistributerCodeNavigation { get; set; }

    public virtual Macrobrick? MacroBrickCodeNavigation { get; set; }

    public virtual Status? Status { get; set; }
}
