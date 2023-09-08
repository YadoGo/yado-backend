using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using yado_backend.Enums;

namespace yado_backend.Models
{
    public class UserRoleRequest
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        [ForeignKey("RequestedRoleId")]
        public int RequestedRoleId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastStatusUpdate { get; set; }

        public UserRoleRequestStatus Status { get; set; } = UserRoleRequestStatus.InProgress;

        [StringLength(500, MinimumLength = 250)]
        public string? Message { get; set; }

        [ForeignKey("ApprovedByUserId")]
        public Guid? ApprovedByUserId { get; set; }

        public User? User { get; set; }

        public Role? RequestedRole { get; set; }

        public User? ApprovedByUser { get; set; }
    }
}

