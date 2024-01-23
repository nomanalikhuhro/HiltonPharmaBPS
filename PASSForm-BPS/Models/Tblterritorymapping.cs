using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Tblterritorymapping
{
    public int TerritoryMappingId { get; set; }

    public string TerritoryCode { get; set; } = null!;

    public int EmpId { get; set; }

    public int RoleId { get; set; }

    public sbyte IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public string? TeamCode { get; set; }

    public virtual User Emp { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual Tblterritory TerritoryCodeNavigation { get; set; } = null!;
}
