using System;
namespace yado_backend.Models.Dtos
{
    public class UserDetailsDto
    {
        public required Guid Id { get; set; }
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Gender { get; set; }
        public required string ImageProfile { get; set; }
        public int RoleId { get; set; }
    }
}

