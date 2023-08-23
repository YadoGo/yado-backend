using System.ComponentModel.DataAnnotations;

namespace yado_backend.Models
{
    public class Role
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        public required string Description { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}

