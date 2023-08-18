using yado_backend.Models;

namespace yado_backend.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsByHotelUuid(string hotelUuid);
        Task<IEnumerable<Review>> GetAllReviewsByUserUuid(string userUuid);
        Task<bool> InsertReview(Review review);
        Task<bool> UpdateReviewById(Review review);
        Task<bool> DeleteReviewById(int reviewId, string userUuid);
    }
}
