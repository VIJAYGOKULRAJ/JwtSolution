using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JwtDemo.Models
{
    public class SavedSearches
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public Guid UserId { get; set; }

        [MaxLength(int.MaxValue)]
        public string SearchQueryJson { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [StringLength(int.MaxValue)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModified { get; set; }

        [StringLength(int.MaxValue)]
        public string LastModifiedBy { get; set; }

        public int SavedSearchViewId { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        [ForeignKey("SavedSearchViewId")]
        public virtual SavedSearchViews views { get; set; }
    }
}