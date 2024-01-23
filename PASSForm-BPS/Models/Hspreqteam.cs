using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Hspreqteam
{
    public int HcpreqTeamId { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? Hcpreqid { get; set; }

    public string? TeamCode { get; set; }

    public virtual Team? TeamCodeNavigation { get; set; }
}
