using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class User
{
    public int EmpId { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public int? RoleId { get; set; }

    public int? LoginId { get; set; }

    public string? UserPassword { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public int? ReportingTo { get; set; }

    public virtual ICollection<Approver> Approvers { get; set; } = new List<Approver>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Tblterritorymapping> Tblterritorymappings { get; set; } = new List<Tblterritorymapping>();
}
