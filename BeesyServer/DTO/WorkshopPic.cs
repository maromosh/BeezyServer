using BeezyServer.Models;
using System.ComponentModel.DataAnnotations;

namespace BeezyServer.DTO
{
    public class WorkshopPic
    {
        public int WorkshopPicId { get; set; }

        public int? WorkshopId { get; set; }

        public string? WorkshopPicEx { get; set; }

        public WorkshopPic(Models.WorkshopPic wp)
        {
            WorkshopPicId = wp.WorkshopPicId;
            WorkshopId = wp.WorkshopId;
            WorkshopPicEx = wp.WorkshopPicEx;
        }

        public Models.WorkshopPic GetModel()
        {
            Models.WorkshopPic wp = new Models.WorkshopPic();
            wp.WorkshopPicId = WorkshopPicId;
            wp.WorkshopId = WorkshopId;
            wp.WorkshopPicEx = WorkshopPicEx;

            return wp;
        }
    }
}
