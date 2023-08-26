using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Review
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public float Qualification { get; set; }

        [Required]
        [StringLength(200)]
        public required string PositiveComment { get; set; }

        [StringLength(200)]
        public required string NegativeComment { get; set; }

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

