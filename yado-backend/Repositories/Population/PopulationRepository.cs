using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class PopulationRepository : IPopulationRepository
    {
        private readonly AppDbContext _dbContext;

        public PopulationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Population>> SearchPopulationsByCityName(string cityName)
        {
            return await _dbContext.Populations
                .Where(p => p.Name.StartsWith(cityName))
                .ToListAsync();
        }
    }
}