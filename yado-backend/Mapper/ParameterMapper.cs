using System;
using AutoMapper;
using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Mapper
{
	public class ParameterMapper : Profile
    {
		public ParameterMapper()
		{
            CreateMap<Parameter, ParameterDto>().ReverseMap();
        }
	}
}

