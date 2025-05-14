using System;
using System.Collections.Generic;

namespace BeezyServer.DTO;

public class Plants
{
    public int PlantId { get; set; }

    public string PlantTopic { get; set; } = null!;

    public string PlantName { get; set; } = null!;

    public string Plantseason { get; set; } = null!;

    public string PlantPic { get; set; } = null!;
    public Plants() { }
    public Plants(Models.Plant p)
    {
        PlantId = p.PlantId;
        PlantTopic = p.PlantTopic;
        PlantName = p.PlantName;
        Plantseason = p.Plantseason;
        PlantPic = p.PlantPic;
    }

    public Models.Plant GetModels()
    {
        return new Models.Plant()
        {
            PlantId = PlantId,
            PlantTopic = PlantTopic,
            PlantName = PlantName,
            Plantseason = Plantseason,
            PlantPic = PlantPic,

        };
    }
}
