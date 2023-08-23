using System;
namespace yado_backend.Models.Dtos
{
	public class UserLoginReponseDto
	{
		public required User User { get; set; }
		public required string Token { get; set; }
	}
}

