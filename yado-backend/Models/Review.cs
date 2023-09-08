using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Review
	{
        [Required]
        [Key]
        public required Guid Id { get; set; }

        public float Qualification { get; set; }

        [Required]
        [StringLength(200)]
        public required string PositiveComment { get; set; }

        [StringLength(200)]
        public required string NegativeComment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public required Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public required User User { get; set; }

        [Required]
        public required Guid HotelId { get; set; }

        [ForeignKey("HotelId")]
        public required Hotel Hotel { get; set; }
    }
}

