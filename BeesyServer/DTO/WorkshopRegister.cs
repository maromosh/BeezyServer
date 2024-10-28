using BeezyServer.Models;

namespace BeezyServer.DTO
{
    public class WorkshopRegister
    {
        public int WorkshopRegisters { get; set; }

        public int? WorkshopId { get; set; }

        public int UserId { get; set; }

        public bool WorkshopRegistersIsPaid { get; set; }

        public WorkshopRegister() { }
        public WorkshopRegister(Models.WorkshopRegister wr)
        {
            WorkshopRegisters = wr.WorkshopRegisters;
            WorkshopId = wr.WorkshopId;
            UserId = wr.UserId;
            WorkshopRegistersIsPaid = wr.WorkshopRegistersIsPaid;
        }

        public Models.WorkshopRegister GetModel()
        {
            Models.WorkshopRegister wr = new Models.WorkshopRegister();
            wr.WorkshopRegisters = WorkshopRegisters;
            wr.WorkshopId = WorkshopId;
            wr.UserId = UserId;
            wr.WorkshopRegistersIsPaid = WorkshopRegistersIsPaid;

            return wr;
        }
    }
}
