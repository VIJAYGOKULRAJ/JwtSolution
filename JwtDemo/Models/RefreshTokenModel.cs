using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JwtDemo.Models
{
    public class RefreshTokenModel
    {
        [Key]
        public string RefreshToken { get; set; }
    }
}