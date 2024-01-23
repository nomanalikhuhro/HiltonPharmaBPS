using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Approver
{
    public int RecordId { get; set; }

    public string? UserRole { get; set; }

    public int? EmpId { get; set; }

    public int? ApproverOrder { get; set; }

    public virtual User? Emp { get; set; }
}
