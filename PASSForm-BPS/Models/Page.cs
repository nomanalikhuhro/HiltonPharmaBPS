using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Page
{
    public int PageId { get; set; }

    public string? PageName { get; set; }

    public string? PagePath { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Roleaccess> Roleaccesses { get; set; } = new List<Roleaccess>();
}
