using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Team
{
    public string TeamCode { get; set; } = null!;

    public string? TeamName { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public sbyte? IsActive { get; set; }

    public virtual ICollection<Hspreqteam> Hspreqteams { get; set; } = new List<Hspreqteam>();
}
