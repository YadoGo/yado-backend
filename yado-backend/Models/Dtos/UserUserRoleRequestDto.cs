using yado_backend.Enums;

namespace yado_backend.Models.Dtos
{
	public class UserUserRoleRequestDto
	{
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int RequestedRoleId { get; set; }
        public UserRoleRequestStatus Status { get; set; }
        public required string Message { get; set; }
    }
}

