using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JwtDemo.Controllers
{
  public enum role
    {
        Admin = 1,
        user = 2
    }


    public class LoginModel
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

       public role Role { get; set; }
       
    }
}