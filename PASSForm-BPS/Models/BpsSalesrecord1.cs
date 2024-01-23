using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class BpsSalesrecord1
{
    public int RecordId { get; set; }

    public int? BpsRecordId { get; set; }

    public string? Month { get; set; }

    public string? ChemistCode { get; set; }

    public string? ProductName { get; set; }

    public int? ActualDiscount { get; set; }

    public string? Contribution { get; set; }

    public string? SalesType { get; set; }

    public int? Sales { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

   

    public virtual Chemist? ChemistCodeNavigation { get; set; }
}
