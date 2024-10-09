using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

[Table("WorkshopPic")]
public partial class WorkshopPic
{
    [Key]
    [Column("WorkshopPicID")]
    public int WorkshopPicId { get; set; }

    public int? WorkshopId { get; set; }

    [StringLength(100)]
    public string? WorkshopPicEx { get; set; }

    [ForeignKey("WorkshopId")]
    [InverseProperty("WorkshopPics")]
    public virtual Workshop? Workshop { get; set; }
}
