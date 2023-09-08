using yado_backend.Enums;

namespace yado_backend.Models.Dtos
{
    public class AdminUserRoleRequestDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public int RequestedRoleId { get; set; }
        public string RequestedRoleName { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime LastStatusUpdate { get; set; }
        public UserRoleRequestStatus Status { get; set; }
        public Guid? ApprovedByUserId { get; set; }
        public string? ApprovedByUsername { get; set; }
        public string? Message { get; set; }
    }
}

