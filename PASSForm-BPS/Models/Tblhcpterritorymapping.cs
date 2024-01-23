using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Tblhcpterritorymapping
{
    public int HcpterritoryMappingId { get; set; }

    public int Hcpid { get; set; }

    public string TerritoryCode { get; set; } = null!;

    public sbyte? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public virtual Hcpdetail Hcp { get; set; } = null!;

    public virtual Tblterritory TerritoryCodeNavigation { get; set; } = null!;
}
