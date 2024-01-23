using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Chemist
{
    public string ChemistCode { get; set; } = null!;

    public string? ChemistName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<MacChemMapping> MacChemMappings { get; set; } = new List<MacChemMapping>();
}
