using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeezyServer.DTO
{
    public class ChatBeekeeper
    {
        public int ChatBeekeepers { get; set; }

        public int BeeKeeperId { get; set; }

        public string ChatBeekeepersText { get; set; } = null!;

        public DateTime ChatBeekeepersTime { get; set; }

        public string? ChatBeekeepersPic { get; set; }

        public ChatBeekeeper() {}

        public ChatBeekeeper(Models.ChatBeekeeper cb)
        {
            ChatBeekeepers = cb.ChatBeekeepers;
            BeeKeeperId = cb.BeeKeeperId;
            ChatBeekeepersText = cb.ChatBeekeepersText;
            ChatBeekeepersTime = cb.ChatBeekeepersTime;
            ChatBeekeepersPic = cb.ChatBeekeepersPic;
        }

        public Models.ChatBeekeeper GetModel()
        {
            Models.ChatBeekeeper cb = new Models.ChatBeekeeper();
            cb.ChatBeekeepers = ChatBeekeepers;
            cb.BeeKeeperId = BeeKeeperId;
            cb.ChatBeekeepersText = ChatBeekeepersText;
            cb.ChatBeekeepersTime = ChatBeekeepersTime;
            cb.ChatBeekeepersPic = ChatBeekeepersPic;

            return cb;
        }
    }
}
