using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

[Table("Report")]
public partial class Report
{
    [Key]
    public int ReportId { get; set; }

    public int UserId { get; set; }

    public int? BeeKeeperId { get; set; }

    [Column(TypeName = "decimal(9, 6)")]
    public decimal? ReportLocation { get; set; }

    [StringLength(2000)]
    public string ReportDirectionsExplanation { get; set; } = null!;

    [StringLength(50)]
    public string ReportUserNumber { get; set; } = null!;

    [StringLength(2000)]
    public string ReportExplanation { get; set; } = null!;

    [ForeignKey("BeeKeeperId")]
    [InverseProperty("Reports")]
    public virtual Beekeeper? BeeKeeper { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Reports")]
    public virtual User User { get; set; } = null!;
}
