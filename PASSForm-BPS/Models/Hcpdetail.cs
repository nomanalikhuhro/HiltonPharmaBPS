using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Hcpdetail
{
    public int Hcpid { get; set; }

    public string? Hcpname { get; set; }

    public string? Pmcnum { get; set; }

    public string? Speciality { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public string? City { get; set; }

    public virtual ICollection<Hcphospital> Hcphospitals { get; set; } = new List<Hcphospital>();

    public virtual ICollection<Tblhcpterritorymapping> Tblhcpterritorymappings { get; set; } = new List<Tblhcpterritorymapping>();
}
