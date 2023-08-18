using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class ParameterRepository : IParameterRepository
    {
        private readonly AppDbContext _dbContext;

        public ParameterRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertParameter(Parameter parameter)
        {
            _dbContext.Parameters.Add(parameter);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateParameterByHotelUuid(string hotelUuid, Parameter parameter)
        {
            var existingParameter = await _dbContext.Parameters.FirstOrDefaultAsync(p => p.HotelUuid == hotelUuid);
            if (existingParameter != null)
            {
                _dbContext.Parameters.Update(existingParameter);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<bool> DeleteParameterByHotelUuid(string hotelUuid)
        {
            var parameter = await _dbContext.Parameters.FirstOrDefaultAsync(p => p.HotelUuid == hotelUuid);
            if (parameter != null)
            {
                _dbContext.Parameters.Remove(parameter);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
