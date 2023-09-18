using AutoMapper;
using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Mapper
{
	public class HotelMapper : Profile
    {
		public HotelMapper()
		{
            CreateMap<Hotel, HotelSummaryDto>().ReverseMap();

            CreateMap<Hotel, HotelSummaryDto>()
                .ForMember(dest => dest.FirstImage, opt => opt.MapFrom(src => src.Images.FirstOrDefault()));

        }
    }
}

