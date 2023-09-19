using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Review
	{
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required Guid Id { get; set; }

        public float Qualification { get; set; }

        [Required]
        [StringLength(500)]
        public required string PositiveComment { get; set; }

        [StringLength(500)]
        public required string NegativeComment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public required Guid HotelId { get; set; }

        public User User { get; set; }
        public Hotel Hotel { get; set; }
    }
}

