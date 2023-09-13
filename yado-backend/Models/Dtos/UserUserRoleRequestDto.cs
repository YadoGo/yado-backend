namespace yado_backend.Models.Dtos
{
	public class UserUserRoleRequestDto
	{
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public string Status { get; set; }
        public required string Message { get; set; }
    }
}

