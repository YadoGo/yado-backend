using yado_backend.Models.Dtos;

namespace yado_backend.Repositories
{
    public interface IPopulationRepository
    {
        Task<IEnumerable<PopulationDto>> SearchPopulationsByCityName(string cityName);
        Task<string> GetPopulationNameById(int id);

    }
}
