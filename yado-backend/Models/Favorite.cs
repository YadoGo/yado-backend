using System.ComponentModel.DataAnnotations;

namespace yado_backend.Models
{
	public class Favorite
	{
        [Key] 
        [StringLength(40)]
        public required Guid UserId { get; set; }

        [Key]
        [StringLength(255)]
        public required Guid HotelId { get; set; }

        public required User User { get; set; }
        public required Hotel Hotel { get; set; }
    }
}

