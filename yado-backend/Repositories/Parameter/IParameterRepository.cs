using System.Threading.Tasks;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IParameterRepository
    {
        Task<bool> InsertParameter(Parameter parameter);
        Task<bool> UpdateParameterByHotelUuid(string hotelUuid, Parameter parameter);
        Task<bool> DeleteParameterByHotelUuid(string hotelUuid);
    }
}
