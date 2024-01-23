using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class TerbrickMapping
{
    public int Id { get; set; }

    public string? TerritoryCode { get; set; }

    public string? MacroBrickCode { get; set; }
    public string? MacroBrickName { get; set; }
    

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual Macrobrick? MacroBrickCodeNavigation { get; set; }

    public virtual Tblterritory? TerritoryCodeNavigation { get; set; }
}
