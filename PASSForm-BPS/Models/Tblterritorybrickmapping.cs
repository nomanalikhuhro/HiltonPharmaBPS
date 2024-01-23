using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Tblterritorybrickmapping
{
    public int TerritoryBrickMappingId { get; set; }

    public string TerritoryCode { get; set; } = null!;

    public string MacrobrickCode { get; set; } = null!;


    public sbyte IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public virtual Tblterritory TerritoryCodeNavigation { get; set; } = null!;
}
