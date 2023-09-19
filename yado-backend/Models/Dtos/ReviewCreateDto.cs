using System.ComponentModel.DataAnnotations;

namespace yado_backend.Models.Dtos
{
    public class ReviewCreateDto
    {
        public float Qualification { get; set; }

        public required string PositiveComment { get; set; }

        public required string NegativeComment { get; set; }

        public Guid UserId { get; set; }
        public Guid HotelId { get; set; }
    }
}

