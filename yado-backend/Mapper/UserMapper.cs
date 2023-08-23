using yado_backend.Models;
using yado_backend.Models.Dtos;
using AutoMapper;

namespace yado_backend.Mapper
{
	public class UserMapper : Profile
	{
		public UserMapper()
		{
            CreateMap<User, UserSummaryDto>().ReverseMap();
            CreateMap<User, UserDetailsDto>().ReverseMap();
        }
	}
}

