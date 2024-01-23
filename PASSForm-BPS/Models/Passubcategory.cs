using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Passubcategory
{
    public string PassubCategoryCode { get; set; } = null!;

    public string? PassubCategoryType { get; set; }

    public string? PasscategoryCode { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }
}
