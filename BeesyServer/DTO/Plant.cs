using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

public partial class Plants
{
    public int PlantId { get; set; }

    public string PlantTopic { get; set; } = null!;

    public string PlantName { get; set; } = null!;

    public string PlantPic { get; set; } = null!;
    public Plants(Models.Plant p)
    {
        PlantId = p.PlantId;
        PlantTopic = p.PlantTopic;
        PlantName = p.PlantName;
        PlantPic = p.PlantPic;
    }
}
