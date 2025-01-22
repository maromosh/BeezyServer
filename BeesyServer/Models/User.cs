using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

[Index("UserPassword", Name = "UQ__Users__5DF58B8149914083", IsUnique = true)]
[Index("UserPhone", Name = "UQ__Users__F2577C47E30BBCB3", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    public string UserEmail { get; set; } = null!;

    [StringLength(50)]
    public string UserPassword { get; set; } = null!;

    [StringLength(50)]
    public string UserPhone { get; set; } = null!;

    [StringLength(50)]
    public string UserCity { get; set; } = null!;

    [StringLength(50)]
    public string UserAddress { get; set; } = null!;

    public bool IsManeger { get; set; }

    [InverseProperty("BeeKeeper")]
    public virtual Beekeeper? Beekeeper { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<ChatQuestionsAswer> ChatQuestionsAswers { get; set; } = new List<ChatQuestionsAswer>();

    [InverseProperty("User")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    [InverseProperty("User")]
    public virtual ICollection<WorkshopRegister> WorkshopRegisters { get; set; } = new List<WorkshopRegister>();
}
