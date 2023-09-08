using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsByHotelId(Guid hotelId);
        Task<IEnumerable<Review>> GetAllReviewsByUserId(Guid userId);
        Task<bool> InsertReview(Review review);
        Task<bool> UpdateReviewById(Review review);
        Task<bool> DeleteReviewById(Guid reviewId, Guid userId);
    }
}
