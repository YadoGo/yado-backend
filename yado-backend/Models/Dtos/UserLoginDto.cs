using System.ComponentModel.DataAnnotations;

namespace yado_backend.Models.Dtos
{
	public class UserLoginDto
	{
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50)]
        public required string Password { get; set; }
    }
}

