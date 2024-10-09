using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

public partial class ChatQuestionsAswer
{
    [Key]
    public int ChatQuestionsAswersId { get; set; }

    public int UserId { get; set; }

    [StringLength(3000)]
    public string ChatQuestionsAswersText { get; set; } = null!;

    [Column("ChatQuestionsAswersTIme", TypeName = "datetime")]
    public DateTime ChatQuestionsAswersTime { get; set; }

    [StringLength(50)]
    public string? ChatQuestionsAswersPic { get; set; }

    [InverseProperty("ChatQuestionsAswers")]
    public virtual ICollection<ChatPic> ChatPics { get; set; } = new List<ChatPic>();

    [ForeignKey("UserId")]
    [InverseProperty("ChatQuestionsAswers")]
    public virtual User User { get; set; } = null!;
}
