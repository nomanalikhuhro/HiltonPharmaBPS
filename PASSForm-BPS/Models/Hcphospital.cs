using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Hcphospital
{
    public int Id { get; set; }

    public int Hcpid { get; set; }

    public int HospId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Hcpdetail Hcp { get; set; } = null!;

    public virtual Hospitaldetail Hosp { get; set; } = null!;
}
