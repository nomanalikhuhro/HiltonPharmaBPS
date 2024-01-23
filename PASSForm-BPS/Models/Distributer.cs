using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Distributer
{
    public string DistributerCode { get; set; } = null!;

    public string? DistrubiterName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<BpsRequest> BpsRequests { get; set; } = new List<BpsRequest>();

    public virtual ICollection<DisterMapping> DisterMappings { get; set; } = new List<DisterMapping>();
}
