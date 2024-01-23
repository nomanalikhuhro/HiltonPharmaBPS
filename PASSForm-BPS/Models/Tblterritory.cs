using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Tblterritory
{
    public string TerritoryCode { get; set; } = null!;

    public sbyte IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public virtual ICollection<DisterMapping> DisterMappings { get; set; } = new List<DisterMapping>();

    public virtual ICollection<Tblhcpterritorymapping> Tblhcpterritorymappings { get; set; } = new List<Tblhcpterritorymapping>();

    public virtual ICollection<Tblterritorymapping> Tblterritorymappings { get; set; } = new List<Tblterritorymapping>();

    public virtual ICollection<TerbrickMapping> TerbrickMappings { get; set; } = new List<TerbrickMapping>();
}
