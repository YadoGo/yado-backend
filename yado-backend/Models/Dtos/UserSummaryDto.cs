namespace yado_backend.Models.Dtos
{
    public class UserSummaryDto
    {
        public required Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Gender { get; set; }
        public required string ImageProfile { get; set; }
        public int RoleId { get; set; }
    }
}

