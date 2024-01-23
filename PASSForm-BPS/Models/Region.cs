using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Region
{
    public string RegionCode { get; set; } = null!;

    public string? RegionName { get; set; }

    public string? ZoneCode { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual Zone? ZoneCodeNavigation { get; set; }
}
