using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class BpsSalesrecord
{
    public int RecordId { get; set; }

    public int? BpsRecordId { get; set; }

    public string? Month { get; set; }

    public string? ChemistCode { get; set; }

    public string? ProductName { get; set; }

    public int? ActualDiscount { get; set; }

    public string? Contribution { get; set; }

    public string? SalesType { get; set; }

    public decimal? SalesSku { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public decimal? SalesValue { get; set; }

    public DateTime? PreFromdate { get; set; }

    public DateTime? PreTodate { get; set; }

    public DateTime? PostFromdate { get; set; }

    public DateTime? PostTodate { get; set; }

    public int? Year { get; set; }

    public string? PackCode { get; set; }

    public string? CostCenter { get; set; }

    public string? PreTotalSal { get; set; }

    public string? PostTotalSal { get; set; }

    public string? Roi { get; set; }

    public string? Description { get; set; }


    public string? DiscountPercentagePre { get; set; }
    public string? TotalRoiPercentage { get; set; }
    public string? TotalWithoutDiscount { get; set; }

    public string? TotalWithDiscount { get; set; }
    public string? BPSPercentage { get; set; }
    public string? DiscountPercentagePost { get; set; }
}
