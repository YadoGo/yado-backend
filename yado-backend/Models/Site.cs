using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Site
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public required string OriginUrl { get; set; }

        [Required]
        public float NightlyPrice { get; set; }

        [Required]
        [ForeignKey("Hotel")]
        [StringLength(40)]
        public required string HotelUuid { get; set; }

        [Required]
        public required Hotel Hotel { get; set; }

        [Required]
        [ForeignKey("Companie")]
        public int CompanyId { get; set; }
      
        [Required]
        public required Company Company { get; set; }
    }
}

