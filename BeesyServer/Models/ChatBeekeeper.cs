using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

public partial class ChatBeekeeper
{
    [Key]
    public int ChatBeekeepers { get; set; }

    public int BeeKeeperId { get; set; }

    [StringLength(3000)]
    public string ChatBeekeepersText { get; set; } = null!;

    [Column("ChatBeekeepersTIme", TypeName = "datetime")]
    public DateTime ChatBeekeepersTime { get; set; }

    [StringLength(100)]
    public string? ChatBeekeepersPic { get; set; }

    [ForeignKey("BeeKeeperId")]
    [InverseProperty("ChatBeekeepers")]
    public virtual Beekeeper BeeKeeper { get; set; } = null!;
}
