using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

[Table("ChatPic")]
public partial class ChatPic
{
    [Key]
    [Column("ChatPicID")]
    public int ChatPicId { get; set; }

    public int? ChatQuestionsAswersId { get; set; }

    [StringLength(50)]
    public string? ChatPicEx { get; set; }

    [ForeignKey("ChatQuestionsAswersId")]
    [InverseProperty("ChatPics")]
    public virtual ChatQuestionsAswer? ChatQuestionsAswers { get; set; }
}
