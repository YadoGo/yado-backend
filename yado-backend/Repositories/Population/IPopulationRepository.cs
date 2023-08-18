using System.Collections.Generic;
using System.Threading.Tasks;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IPopulationRepository
    {
        Task<IEnumerable<Population>> GetAllPopulations();
    }
}
