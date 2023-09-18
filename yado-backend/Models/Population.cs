using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Population
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        [Required]
        [StringLength(2)]
        public required string CountryCode { get; set; }

        [ForeignKey("CountryCode")]
        public virtual required Country Country { get; set; }

        public List<Hotel> Hotels { get; set; } = new List<Hotel>();

    }
}

