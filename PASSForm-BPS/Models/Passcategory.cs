using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Passcategory
{
    public string PasscategoryCode { get; set; } = null!;

    public string? PasscategoryType { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }
}
