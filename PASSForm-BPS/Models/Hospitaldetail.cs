using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Hospitaldetail
{
    public int HospId { get; set; }

    public string? HospitalName { get; set; }

    public string? HospitalAddress { get; set; }

    public string? HospitalCity { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Hcphospital> Hcphospitals { get; set; } = new List<Hcphospital>();
}
