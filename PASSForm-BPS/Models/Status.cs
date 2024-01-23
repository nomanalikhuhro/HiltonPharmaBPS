using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string? StatusType { get; set; }

    public virtual ICollection<BpsRequest> BpsRequests { get; set; } = new List<BpsRequest>();
}
