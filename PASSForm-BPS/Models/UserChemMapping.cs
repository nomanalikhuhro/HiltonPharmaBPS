using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class UserChemMapping
{
    public int Id { get; set; }

    public string? ChemistCode { get; set; }
    public string? ChemistName { get; set; }
    

    public int? EmpId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Chemist? ChemistCodeNavigation { get; set; }

    public virtual User? Emp { get; set; }

    public List<DsrHiltonDailySalesTeamToChemist202223> dsrHiltonDailySalesTeamToChemists { get; set; }


}
