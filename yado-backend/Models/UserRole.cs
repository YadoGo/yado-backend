using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace yado_backend.Models
{

    [Index(nameof(UserId), nameof(RoleId), IsUnique = true)]
    public class UserRole
    {
        [Key]
        public Guid UserId { get; set; }

        [Key]
        public int RoleId { get; set; } = 1;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
    }
}

