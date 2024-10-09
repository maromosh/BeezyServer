using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

[Table("Workshop")]
public partial class Workshop
{
    [Key]
    public int WorkshopId { get; set; }

    public int BeeKeeperId { get; set; }

    [StringLength(100)]
    public string WorkshopName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime WorkshopDate { get; set; }

    [StringLength(100)]
    public string WorkshopPrice { get; set; } = null!;

    public int WorkshopMaxReg { get; set; }

    [StringLength(4000)]
    public string WorkshopDescription { get; set; } = null!;

    [ForeignKey("BeeKeeperId")]
    [InverseProperty("Workshops")]
    public virtual Beekeeper BeeKeeper { get; set; } = null!;

    [InverseProperty("Workshop")]
    public virtual ICollection<WorkshopPic> WorkshopPics { get; set; } = new List<WorkshopPic>();

    [InverseProperty("Workshop")]
    public virtual ICollection<WorkshopRegister> WorkshopRegisters { get; set; } = new List<WorkshopRegister>();
}
