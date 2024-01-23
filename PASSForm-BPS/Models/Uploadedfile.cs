using System;
using System.Collections.Generic;

namespace PASSForm_BPS.Models;

public partial class Uploadedfile
{
    public int Fileid { get; set; }

    public string? TrackingId { get; set; }

    public string? filename { get; set; }
    public string? filepath { get; set; }

    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int? IsDeleted { get; set; }
    public DateTime? IsDeletedOn { get; set; }
    public int? IsDeletedBy { get; set; }
    public int HCPREQID { get; set; }
    public int BPSID { get; set; }
    public string? Filetype { get; set; }
    public string? CategoryCode { get; set; }
    public int? ActivityBy { get; set; }
}
