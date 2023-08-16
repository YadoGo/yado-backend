using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class State
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public required string Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual required Country Country { get; set; }

        public List<Population> Populations { get; set; } = new List<Population>();
    }
}

