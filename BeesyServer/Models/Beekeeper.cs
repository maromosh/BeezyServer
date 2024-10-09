using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

[Table("Beekeeper")]
public partial class Beekeeper
{
    [Key]
    public int BeeKeeperId { get; set; }

    public int BeekeeperRadius { get; set; }

    [StringLength(50)]
    public string BeekeeperKind { get; set; } = null!;

    public bool BeekeeperIsActive { get; set; }

    [ForeignKey("BeeKeeperId")]
    [InverseProperty("Beekeeper")]
    public virtual User BeeKeeper { get; set; } = null!;

    [InverseProperty("BeeKeeper")]
    public virtual ICollection<ChatBeekeeper> ChatBeekeepers { get; set; } = new List<ChatBeekeeper>();

    [InverseProperty("BeeKeeper")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    [InverseProperty("BeeKeeper")]
    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
