using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _dbContext;

        public OwnerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Owner>> GetAllOwners()
        {
            var owners = await _dbContext.Owners.ToListAsync();
            return owners;
        }

        public async Task<bool> InsertOwner(Owner owner)
        {
            _dbContext.Owners.Add(owner);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteOwnerByID(Guid ownerId)
        {
            var owner = await _dbContext.Owners.FindAsync(ownerId);
            if (owner != null)
            {
                _dbContext.Owners.Remove(owner);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }

        public async Task<IEnumerable<Hotel>> GetHotelsByOwnerId(Guid ownerId)
        {
            return await _dbContext.Owners
                .Where(o => o.UserId == ownerId)
                .Select(o => o.Hotel)
                .ToListAsync();
        }
    }
}
