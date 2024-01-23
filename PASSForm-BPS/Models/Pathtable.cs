using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Pathtable
{
    public int Pathid { get; set; }

    public string? Pathdescription { get; set; }

    public string FileName { get; set; }
    public string? CategoryCode { get; set; }
}
