using System;
using System.ComponentModel.DataAnnotations;

namespace yado_backend.Models.Dtos
{
	public class ReviewUpdateDto
	{
        public float Qualification { get; set; }
        public required string PositiveComment { get; set; }
        public required string NegativeComment { get; set; }
    }
}

