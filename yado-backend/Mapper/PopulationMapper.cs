using AutoMapper;
using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Mapper
{
	public class PopulationMapper : Profile 
	{
		public PopulationMapper()
		{
			CreateMap<Population, PopulationDto>().ReverseMap();
		}
	}
}

