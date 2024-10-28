using BeezyServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeezyServer.DTO
{
    public class BeeKeeper : User
    {
        public int BeeKeeperId { get; set; }

        public int BeekeeperRadius { get; set; }

        public string BeekeeperKind { get; set; } = null!;

        public bool BeekeeperIsActive { get; set; }

        public BeeKeeper() { }

        public BeeKeeper(Models.Beekeeper b) : base(b.BeeKeeper) // Calls the UserDto constructor
        {
            BeekeeperRadius = b.BeekeeperRadius;
            BeekeeperKind = b.BeekeeperKind;
            BeekeeperIsActive = b.BeekeeperIsActive;
        }

        public Models.Beekeeper GetModel()
        {
            var beekeeper = new Models.Beekeeper
            {
                BeekeeperRadius = this.BeekeeperRadius,
                BeekeeperKind = this.BeekeeperKind,
                BeekeeperIsActive = this.BeekeeperIsActive,
                BeeKeeper = base.GetModel(),
                BeeKeeperId = this.BeeKeeperId
            };
            return beekeeper;
        }
    }
}
