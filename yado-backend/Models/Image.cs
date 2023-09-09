using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Image
	{
        [Required]
        [Key]
        public required Guid Id { get; set; }

        [Required]
        [StringLength(5000)]
        public required string ImagePath { get; set; }

        [Required]
        [StringLength(200)]
        public required string Description { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public required Guid HotelId { get; set; }

        [ForeignKey("HotelId")]
        public required Hotel Hotel { get; set; }
    }
}

