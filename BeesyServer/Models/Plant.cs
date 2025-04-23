using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

public partial class Plant
{
    [Key]
    public int PlantId { get; set; }

    [StringLength(50)]
    public string PlantTopic { get; set; } = null!;

    [StringLength(50)]
    public string PlantName { get; set; } = null!;

    [StringLength(150)]
    public string PlantPic { get; set; } = null!;
}
