using AutoMapper;
using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Mapper
{
	public class UserRoleRequestMapper : Profile
    {
		public UserRoleRequestMapper()
		{
            CreateMap<UserRoleRequest, UserUserRoleRequestDto>().ReverseMap();
            CreateMap<UserRoleRequest, AdminUserRoleRequestDto>().ReverseMap();
        }
	}
}

