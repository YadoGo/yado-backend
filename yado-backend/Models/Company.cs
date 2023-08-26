using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Company
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(200)]
        public required string Url { get; set; }

        [Required]
        [Column(TypeName = "varbinary(8000)")]
        public required string LogoImg { get; set; }

        public ICollection<Site> Sites { get; set; } = new List<Site>();
    }
}

