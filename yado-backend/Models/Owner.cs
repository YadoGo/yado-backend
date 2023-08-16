using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Owner
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public required string UserUuid { get; set; }

        [Required]
        public required string HotelUuid { get; set; }

        [ForeignKey("UserUuid")]
        public required virtual User User { get; set; }

        [ForeignKey("HotelUuid")]
        public required virtual Hotel Hotel { get; set; }

    }
}

