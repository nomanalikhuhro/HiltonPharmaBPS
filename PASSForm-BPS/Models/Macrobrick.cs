using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Macrobrick
{
    public string MacroBrickCode { get; set; } = null!;

    public string? MacroBrickName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<BpsRequest> BpsRequests { get; set; } = new List<BpsRequest>();

    public virtual ICollection<MacChemMapping> MacChemMappings { get; set; } = new List<MacChemMapping>();

    public virtual ICollection<TerbrickMapping> TerbrickMappings { get; set; } = new List<TerbrickMapping>();
}
