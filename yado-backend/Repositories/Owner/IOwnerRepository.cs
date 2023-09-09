using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllOwners();

        Task<bool> InsertOwner(Owner owner);

        Task<bool> DeleteOwnerByID(Guid ownerId);
    }
}
