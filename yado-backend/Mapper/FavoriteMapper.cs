using AutoMapper;
using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Mapper
{
	public class FavoriteMapper : Profile
    {
		public FavoriteMapper()
		{
            CreateMap<Favorite, FavoriteRequestDto>().ReverseMap();
        }
	}
}

