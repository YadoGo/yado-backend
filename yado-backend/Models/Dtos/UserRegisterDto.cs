using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace yado_backend.Models.Dtos
{
	public class UserRegisterDto
	{
        [Required(ErrorMessage = "Username is required")]
        [StringLength(100)]
        public required string Username { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(100)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50)]
        public required string Password { get; set; }
    }
}

