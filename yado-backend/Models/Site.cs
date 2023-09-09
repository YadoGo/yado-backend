using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Site
	{
        [Required]
        [Key]
        public required Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string TypeRomm { get; set; }

        [Required]
        [StringLength(700)]
        public required string OriginUrl { get; set; }

        [Required]
        public float NightlyPrice { get; set; }

        [Required]
        [ForeignKey("Hotel")]
        public required Guid HotelId { get; set; }

        [Required]
        public required Hotel Hotel { get; set; }

        [Required]
        [ForeignKey("Companie")]
        public int CompanyId { get; set; }
      
        [Required]
        public required Company Company { get; set; }
    }
}

