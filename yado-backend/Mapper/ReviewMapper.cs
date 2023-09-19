using System;
using AutoMapper;
using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Mapper
{
	public class ReviewMapper : Profile
    {
		public ReviewMapper()
		{
            CreateMap<Review, ReviewCreateDto>().ReverseMap();
			CreateMap<Review, ReviewDto>().ReverseMap();
        }
	}
}

