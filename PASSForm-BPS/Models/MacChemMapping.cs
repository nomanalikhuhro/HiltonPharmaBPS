using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class MacChemMapping
{
    public int Id { get; set; }

    public string? ChemistCode { get; set; }
    public string? ChemistName { get; set; }
    

    public string? MacroBrickCode { get; set; }

    public bool? IsActive { get; set; }

    public virtual Chemist? ChemistCodeNavigation { get; set; }

    public virtual Macrobrick? MacroBrickCodeNavigation { get; set; }
}
