using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Roleaccess> Roleaccesses { get; set; } = new List<Roleaccess>();

    public virtual ICollection<Tblterritorymapping> Tblterritorymappings { get; set; } = new List<Tblterritorymapping>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
