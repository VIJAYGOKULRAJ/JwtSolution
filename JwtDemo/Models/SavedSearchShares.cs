using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace JwtDemo.Models
{
    public class SavedSearchShares
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int SavedSearchId { get; set; }


        public Guid SharedUserId { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [StringLength(int.MaxValue)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModified { get; set; }

        [StringLength(int.MaxValue)]
        public string LastModifiedBy { get; set; }


        [ForeignKey("SharedUserId")]
        public virtual Users User { get; set; }

        [ForeignKey("SavedSearchId")]
        public virtual SavedSearches savedsearch { get; set; }
    }
}