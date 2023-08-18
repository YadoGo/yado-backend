using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace yado_backend.Models
{
    public class User : IdentityUser
    {
        [Required]
        [Key]
        [StringLength(36)]
        public required string UUID { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public new required string Email { get; set; }


        [Required]
        [StringLength(50)]
        public required string Password { get; set; }

        [StringLength(50)]
        public string? Gender { get; set; }

        [Column(TypeName = "varbinary(8000)")]
        [StringLength(50)]
        public string? ImageProfile { get; set; }


        [ForeignKey("Role")]
        [Range(1, 3, ErrorMessage = "Role ID must be between 1 and 3.")]
        [DefaultValue(1)]
        public int? RoleId { get; set; }

        public required Role Role { get; set; }

        public virtual List<Owner> OwnedHotels { get; set; } = new List<Owner>();

        public virtual List<Favorite> Favorites { get; set; } = new List<Favorite>();

        public virtual List<Review> Reviews { get; set; } = new List<Review>();

    }
}

