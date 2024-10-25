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

        public Beekeeper(Models.Beekeeper b) : base(b) // Calls the UserDto constructor
        {
            BeekeeperRadius = b.BeekeeperRadius;
            BeekeeperKind = b.BeekeeperKind;
            BeekeeperIsActive = b.BeekeeperIsActive;
        }

        public Models.Beekeeper GetModel()
        {
            var beekeeper = new Models.Beekeeper
            {
                UserId = UserId,
                UserName = UserName,
                UserEmail = UserEmail,
                UserPassword = UserPassword,
                UserPhone = UserPhone,
                UserCity = UserCity,
                UserAddress = UserAddress,
                IsManager = IsManager,
                BeekeeperRadius = BeekeeperRadius,
                BeekeeperKind = BeekeeperKind,
                BeekeeperIsActive = BeekeeperIsActive
            };
            return beekeeper;
        }
        public Models.Beekeeper GetModel()
        {
            Models.Beekeeper b = new Models.Beekeeper();
            b.BeeKeeperId = UserId; // Inherits UserId from UserDto
            b.BeeKeeperRadius = BeekeeperRadius;
            b.BeeKeeperKind = BeekeeperKind;
            b.BeeKeeperIsActive = BeekeeperIsActive;

            return b;
        }

    }
}
