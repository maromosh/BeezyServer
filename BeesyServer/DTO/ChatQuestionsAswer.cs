using BeezyServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeezyServer.DTO
{
    public class ChatQuestionsAswer
    {
        public int ChatQuestionsAswersId { get; set; }

        public int UserId { get; set; }

        public string ChatQuestionsAswersText { get; set; } = null!;

        public DateTime ChatQuestionsAswersTime { get; set; }

        public string? ChatQuestionsAswersPic { get; set; }
        public ChatQuestionsAswer(Models.ChatQuestionsAswer cqa)
        {
            ChatQuestionsAswersId = cqa.ChatQuestionsAswersId;
            UserId = cqa.UserId;
            ChatQuestionsAswersText = cqa.ChatQuestionsAswersText;
            ChatQuestionsAswersTime = cqa.ChatQuestionsAswersTime;
            ChatQuestionsAswersPic = cqa.ChatQuestionsAswersPic;
        }

        public Models.ChatQuestionsAswer GetModel()
        {
            Models.ChatQuestionsAswer cqa = new Models.ChatQuestionsAswer();
            cqa.ChatQuestionsAswersId = ChatQuestionsAswersId;
            cqa.UserId = UserId;
            cqa.ChatQuestionsAswersText = ChatQuestionsAswersText;
            cqa.ChatQuestionsAswersTime = ChatQuestionsAswersTime;
            cqa.ChatQuestionsAswersPic = ChatQuestionsAswersPic;

            return cqa;
        }
    }
}
