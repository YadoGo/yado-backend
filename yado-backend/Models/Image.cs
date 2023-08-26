using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Image
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varbinary(8000)")]
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

