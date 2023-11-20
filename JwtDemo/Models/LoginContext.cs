using JwtDemo.Controllers;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JwtDemo.Models
{
    public class LoginContext : DbContext
    {
        public LoginContext() : base("connection")
        {

        }
        public DbSet<LoginModel> user { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<StudentDetails> Student { get; set; }

        public DbSet<SavedSearchViews> SavedSearchViews { get; set; }
        public DbSet<SavedSearches> SavedSearches { get; set; }

        public DbSet<SavedSearchShares> SavedSearchShares { get; set; }


        public DbSet<RefreshTokenModel> RefreshToken { get; set; }
    }
}