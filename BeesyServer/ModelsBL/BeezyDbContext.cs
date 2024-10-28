using System;
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
}
