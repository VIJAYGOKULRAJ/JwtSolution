using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtDemo.Models
{
    public class SavedSearchViews
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(int.MaxValue)]
        public string FieldNameJson { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        [StringLength(int.MaxValue)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime LastModified { get; set; }
        [StringLength(int.MaxValue)]
        public string LastModifiedBy { get; set; }
        [StringLength(int.MaxValue)]
        public string Name { get; set; }
        [Required]

        public Guid UserId { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        public string Type { get; set; }


        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}