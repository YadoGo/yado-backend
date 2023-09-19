using yado_backend.Models.Dtos;

namespace yado_backend.Repositories
{
	public interface IParameterRepository
	{
        Task<ParameterDto> GetParametersByHotelIdAsync(Guid hotelId);

    }
}

