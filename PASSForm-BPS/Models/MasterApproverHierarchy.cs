using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class MasterApproverHierarchy
{
    public int HierarchyId { get; set; }

    public string? UserRole { get; set; }

    public string? Roles { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
