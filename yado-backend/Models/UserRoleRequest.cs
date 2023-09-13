using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models
{
    public class UserRoleRequest
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int RoleId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastStatusUpdate { get; set; }

        public string Status { get; set; }

        [StringLength(500, MinimumLength = 250)]
        public string? Message { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("RoleId")]
        public Role? RequestedRole { get; set; }

        [ForeignKey("ApprovedByUserId")]
        public Guid? ApprovedByUserId { get; set; }

        public User? ApprovedByUser { get; set; }
    }
}

