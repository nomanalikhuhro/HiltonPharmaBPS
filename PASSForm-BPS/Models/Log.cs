using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Log
{
    public int ApproverLogId { get; set; }

    public string? ApproverLog { get; set; }

    public string? UserRole { get; set; }

    public string? ApprovalActivity { get; set; }

    public string? ActivityBy { get; set; }

    public DateTime? ActivityOn { get; set; }
}
