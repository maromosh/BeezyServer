using BeezyServer.Models;
using System.ComponentModel.DataAnnotations;

namespace BeezyServer.DTO
{
    public class ChatPic
    {
        public int ChatPicId { get; set; }

        public int? ChatQuestionsAswersId { get; set; }

        public string? ChatPicEx { get; set; }
        public ChatPic(Models.ChatPic cp)
        {
            ChatPicId = cp.ChatPicId;
            ChatQuestionsAswersId = cp.ChatQuestionsAswersId;
            ChatPicEx = cp.ChatPicEx;
        }

        public Models.ChatPic GetModel()
        {
            Models.ChatPic cp = new Models.ChatPic();
            cp.ChatPicId = ChatPicId;
            cp.ChatQuestionsAswersId = ChatQuestionsAswersId;
            cp.ChatPicEx = ChatPicEx;

            return cp;
        }
    }
}
