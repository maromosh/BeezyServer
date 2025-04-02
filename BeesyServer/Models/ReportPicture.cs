using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

public partial class ReportPicture
{
    [Key]
    public int PicId { get; set; }

    public int? ReportId { get; set; }

    [ForeignKey("ReportId")]
    [InverseProperty("ReportPictures")]
    public virtual Report? Report { get; set; }
}
