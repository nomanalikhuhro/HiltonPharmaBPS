using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Tblteamsmapping
{
    public int Id { get; set; }

    public string? TeamCode { get; set; }

    public int? EmpId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? IsActive { get; set; }

    public ulong? IsDeletedBy { get; set; }

    public DateTime? IsDeletedOn { get; set; }
}
