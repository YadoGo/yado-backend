using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Favorite
	{
        [Key] 
        [StringLength(40)]
        public required string UserUuid { get; set; }

        [Key]
        [StringLength(255)]
        public required string HotelUuid { get; set; }

        public required User User { get; set; }
        public required Hotel Hotel { get; set; }
    }
}

