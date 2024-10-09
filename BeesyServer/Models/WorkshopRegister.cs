using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

public partial class WorkshopRegister
{
    [Key]
    public int WorkshopRegisters { get; set; }

    public int? WorkshopId { get; set; }

    public int UserId { get; set; }

    public bool WorkshopRegistersIsPaid { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("WorkshopRegisters")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("WorkshopId")]
    [InverseProperty("WorkshopRegisters")]
    public virtual Workshop? Workshop { get; set; }
}
