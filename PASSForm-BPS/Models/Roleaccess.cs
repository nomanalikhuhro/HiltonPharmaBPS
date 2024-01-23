using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Roleaccess
{
    public int RoleAccessId { get; set; }

    public int? PageId { get; set; }

    public int? RoleId { get; set; }

    public bool? IsView { get; set; }

    public bool? IsUpdate { get; set; }

    public bool? IsDelete { get; set; }

    public bool? IsInsert { get; set; }

    public virtual Page? Page { get; set; }

    public virtual Role? Role { get; set; }
}
