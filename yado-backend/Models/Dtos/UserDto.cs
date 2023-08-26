using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yado_backend.Models.Dtos
{
	public class UserDto
	{
        [Required]
        public required Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Username { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }


        [Required]
        [StringLength(50)]
        public required string Password { get; set; }

        [StringLength(50)]
        public string? Gender { get; set; }

        [Column(TypeName = "varbinary(8000)")]
        public string? ImageProfile { get; set; }

        [Range(1, 3, ErrorMessage = "Role ID must be between 1 and 3.")]
        [DefaultValue(1)]
        public int RoleId { get; set; }
    }
}

