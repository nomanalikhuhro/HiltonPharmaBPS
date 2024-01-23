using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Exceptionlog
{
    public int Id { get; set; }

    public string? LogEvent { get; set; }

    public string? Exception { get; set; }

    public string Message { get; set; } = null!;

    public DateTime TimeStamp { get; set; }
}
