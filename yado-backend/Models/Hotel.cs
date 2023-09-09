using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
	public class Hotel
	{
        [Key]
        [Required]
        public required Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(5000)]
        public required string Description { get; set; }

        [Required]
        public int Stars { get; set; }

        [Required]
        [StringLength(500)]
        public required string Address { get; set; }

        [Required]
        public float Latitude { get; set; }

        [Required]
        public float Longitude { get; set; }

        public int NumVisited { get; set; } = 0;

        [ForeignKey("Population")]
        public int PopulationId { get; set; }

        public virtual required Population Population { get; set; }

        public virtual required Parameter Parameter { get; set; }

        public virtual List<Owner> Owners { get; set; } = new List<Owner>();

        public virtual List<Favorite> Favorites { get; set; } = new List<Favorite>();

        public virtual List<Review> Reviews { get; set; } = new List<Review>();

        public virtual List<Image> Images { get; set; } = new List<Image>();

        public virtual List<Site> Sites { get; set; } = new List<Site>();
    }
}

