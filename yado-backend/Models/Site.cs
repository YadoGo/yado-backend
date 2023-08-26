using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Site
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
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

