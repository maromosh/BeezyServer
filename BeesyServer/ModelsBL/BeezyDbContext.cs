using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

public partial class BeezyDbContext : DbContext
{
    //יודע לגשת לכל הטבלאות במסד הנתונים לפי נתיב ולפי סוג הפעולה 
    public Models.User? GetUser(string email)
    {
        //מחזיר משתמש לפי אימייל
        //למה זה חשוב? כדי לבדוק אם מישהו כבר קיים במערכת (למשל בעת התחברות או הרשמה).
        return this.Users.Where(u => u.UserEmail == email)
                           .FirstOrDefault(); // Adjust based on your actual data access method
    }

    public Models.Beekeeper? GetBeekeeper(int id)
    {
        //מחזיר דבוראי לפי מזהה
        return this.Beekeepers.Where(u => u.BeeKeeperId == id)
                           .FirstOrDefault(); // Adjust based on your actual data access method
    }
    public List<User> GetUsers()
    {
        //מחזיר את כל המשתמשים
        return this.Users.ToList();
    }
    public List<Beekeeper> GetBeekepers()
    {
        //מחזיר את כל הדבוראים
        return this.Beekeepers.ToList();
    }
    public List<Report> GetReports()
    {
        //מחזיר את כל הדיווחים
        return this.Reports.Include(r => r.ReportPictures).ToList();
    }
}
