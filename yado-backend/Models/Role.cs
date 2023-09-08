using System.ComponentModel.DataAnnotations;

namespace yado_backend.Models
{
    public class Role
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        public required string Description { get; set; }

        public required ICollection<UserRole> UserRoles { get; set; }
    }
}

