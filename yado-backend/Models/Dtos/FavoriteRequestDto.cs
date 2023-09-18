using System.ComponentModel.DataAnnotations;

namespace yado_backend.Models.Dtos
{
	public class FavoriteRequestDto
	{
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid HotelId { get; set; }
    }
}

