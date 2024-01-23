using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Zone
{
    public string ZoneCode { get; set; } = null!;

    public string? ZoneName { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
}
