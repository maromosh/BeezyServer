using BeezyServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeezyServer.DTO
{
    public class Workshop
    {
        public int WorkshopId { get; set; }

        public int BeeKeeperId { get; set; }

        public string WorkshopName { get; set; } = null!;

        public DateTime WorkshopDate { get; set; }

        public string WorkshopPrice { get; set; } = null!;

        public int WorkshopMaxReg { get; set; }

        public string WorkshopDescription { get; set; } = null!;

        public Workshop() { }
        public Workshop(Models.Workshop w)
        {
            WorkshopId = w.WorkshopId;
            BeeKeeperId = w.BeeKeeperId;
            WorkshopName = w.WorkshopName;
            WorkshopDate = w.WorkshopDate;
            WorkshopPrice = w.WorkshopPrice;
            WorkshopMaxReg = w.WorkshopMaxReg;
            WorkshopDescription = w.WorkshopDescription;
        }

        public Models.Workshop GetModel()
        {
            Models.Workshop w = new Models.Workshop();
            w.WorkshopId = WorkshopId;
            w.BeeKeeperId = BeeKeeperId;
            w.WorkshopName = WorkshopName;
            w.WorkshopDate = WorkshopDate;
            w.WorkshopPrice = WorkshopPrice;
            w.WorkshopMaxReg = WorkshopMaxReg;
            w.WorkshopDescription = WorkshopDescription;

            return w;
        }
    }
}
