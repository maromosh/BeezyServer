using BeezyServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeezyServer.DTO
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public string UserPhone { get; set; } = null!;

        public string UserCity { get; set; } = null!;

        public string UserAddress { get; set; } = null!;

        public bool IsManeger { get; set; }

        public string ProfileImagePath { get; set; } = null!;

        public User() { }
        public User(Models.User u)
        {
            UserId = u.UserId;
            UserName = u.UserName;
            UserEmail = u.UserEmail;
            UserPassword = u.UserPassword;
            UserPhone = u.UserPhone;
            UserCity = u.UserCity;
            UserAddress = u.UserAddress;
            IsManeger = u.IsManeger;
        }
        
        public Models.User GetModel()
        {
            Models.User u = new Models.User();
            u.UserId = UserId;
            u.UserName = UserName;
            u.UserEmail = UserEmail;
            u.UserPassword = UserPassword;
            u.UserPhone = UserPhone;
            u.UserCity = UserCity;
            u.UserAddress = UserAddress;
            u.IsManeger = IsManeger;

            return u;
        }
    }
}
