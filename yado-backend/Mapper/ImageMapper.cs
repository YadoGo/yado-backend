using System;
using AutoMapper;
using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Mapper
{
	public class ImageMapper : Profile
    {
		public ImageMapper()
		{
            CreateMap<Image, ImageDto>().ReverseMap();
        }
	}
}

