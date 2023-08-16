using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Population
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }

        public virtual required State State { get; set; }

        public List<Hotel> Hotels { get; set; } = new List<Hotel>();

    }
}

