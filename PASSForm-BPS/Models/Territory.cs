using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Territory
{
    public string TerritorieCode { get; set; } = null!;

    public string? TerritorieName { get; set; }

    public string? RegionCode { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual Region? RegionCodeNavigation { get; set; }
}
