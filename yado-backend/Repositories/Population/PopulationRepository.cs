using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Repositories
{
    public class PopulationRepository : IPopulationRepository
    {
        private readonly AppDbContext _dbContext;

        public PopulationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PopulationDto>> SearchPopulationsByCityName(string cityName)
        {
            var populations = await _dbContext.Populations
                .Include(p => p.Country)
                .Where(p => p.Name.StartsWith(cityName))
                .ToListAsync();

            var populationDtos = populations.Select(population => new PopulationDto
            {
                Id = population.Id,
                Name = $"{population.Name}, {population.Country.Name}"
            });

            return populationDtos;
        }
    }
}