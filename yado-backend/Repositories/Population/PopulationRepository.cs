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
            var parts = cityName.Split(',');

            if (parts.Length == 1)
            {
                var populations = await _dbContext.Populations
                    .Include(p => p.Country)
                    .Where(p => p.Name.StartsWith(parts[0].Trim()))
                    .ToListAsync();

                var populationDtos = populations.Select(population => new PopulationDto
                {
                    Id = population.Id,
                    Name = $"{population.Name}, {population.Country.Name}",
                    Latitude = population.Latitude,
                    Longitude = population.Longitude
                });

                return populationDtos;
            }
            else if (parts.Length == 2)
            {
                var city = parts[0].Trim();
                var country = parts[1].Trim();

                var populations = await _dbContext.Populations
                    .Include(p => p.Country)
                    .Where(p => p.Name.StartsWith(city) && p.Country.Name.StartsWith(country))
                    .ToListAsync();

                var populationDtos = populations.Select(population => new PopulationDto
                {
                    Id = population.Id,
                    Name = $"{population.Name}, {population.Country.Name}",
                    Latitude = population.Latitude,
                    Longitude = population.Longitude
                });

                return populationDtos;
            }
            else
            {
                throw new ArgumentException("Ambiguous search, too many commas in the string.");
            }
        }

        public async Task<string> GetPopulationNameById(int id)
        {
            var population = await _dbContext.Populations.FindAsync(id);

            if (population == null)
            {
                return null;
            }

            return population.Name;
        }

    }
}