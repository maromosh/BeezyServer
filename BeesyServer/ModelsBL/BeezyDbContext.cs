﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

public partial class BeezyDbContext : DbContext
{
    public Models.User? GetUser(string email)
    {
        return this.Users.Where(u => u.UserEmail == email)
                           .FirstOrDefault(); // Adjust based on your actual data access method
    }

    public Models.Beekeeper? GetBeekeeper(int id)
    {
        return this.Beekeepers.Where(u => u.BeeKeeperId == id)
                           .FirstOrDefault(); // Adjust based on your actual data access method
    }
    public List<User> GetUsers()
    {
        return this.Users.ToList();
    }
    public List<Beekeeper> GetBeekepers()
    {
        return this.Beekeepers.ToList();
    }
    public List<Report> GetReports()
    {
        return this.Reports.Include(r => r.ReportPictures).ToList();
    }
}
