using System;
using System.ComponentModel.DataAnnotations;

namespace yado_backend.Models.Dtos
{
	public class UserChangePasswordDto
	{
        [Required]
        public required string CurrentPassword { get; set; }

        [Required]
        public required string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }
    }
}

