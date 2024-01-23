using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class DisterMapping
{
    public int Id { get; set; }

    public string? TerritoryCode { get; set; }

    public string? DistributerCode { get; set; }
    public string? DistrubiterName { get; set; }
    

    public bool? IsActive { get; set; }

    public virtual Distributer? DistributerCodeNavigation { get; set; }

    public virtual Tblterritory? TerritoryCodeNavigation { get; set; }
}
