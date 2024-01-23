using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Hcprequest
{
    public int Hcpreqid { get; set; }

    public string? Asmcode { get; set; }

    public string? Tmcode { get; set; }

    public string? BaseArea { get; set; }

    public string? TmempNo { get; set; }

    public int? Hcpid { get; set; }

    public int? HospId { get; set; }

    public string? DescriptionOfPlan { get; set; }

    public string? EstimatedSupport { get; set; }

    public string? VendorDetails { get; set; }

    public string? BeneficiaryName { get; set; }

    public string? Ntnnumber { get; set; }

    public string? PaymentMode { get; set; }

    public string? CostCenter { get; set; }

    public bool? IsActive { get; set; }

    public int? StatusId { get; set; }

    public string? PendingTo { get; set; }

    public string? Comments { get; set; }

    public string? PasscategoryCode { get; set; }

    public sbyte? PrevPlan { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CurrentApproverEmpId { get; set; }

    public string? PassubCategoryCode { get; set; }

    public string TrackingId { get; set; } = null!;

    public string? TeamId { get; set; }
}
