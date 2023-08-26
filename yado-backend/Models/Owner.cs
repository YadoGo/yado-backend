using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Owner
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public required Guid HotelId { get; set; }

        [ForeignKey("UserId")]
        public required virtual User User { get; set; }

        [ForeignKey("HotelId")]
        public required virtual Hotel Hotel { get; set; }

    }
}

