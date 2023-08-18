using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Image
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "varbinary(8000)")]
        [StringLength(50)]
        public string? ImagePath { get; set; }

        [Required]
        [StringLength(200)]
        public required string Description { get; set; }

        [Required]
        public required string HotelUuid { get; set; }

        [ForeignKey("HotelUuid")]
        public required Hotel Hotel { get; set; }
    }
}

