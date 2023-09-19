using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsByHotelId(Guid hotelId);
        Task<int> GetReviewCountByHotelId(Guid hotelId);
        Task<string> GetAverageRatingByHotelId(Guid hotelId);
        Task<IEnumerable<Review>> GetAllReviewsByUserId(Guid userId);
        Task<bool> InsertReview(ReviewCreateDto reviewCreateDto);
        Task<bool> UpdateReviewById(Review review);
        Task<bool> DeleteReviewById(Guid reviewId, Guid userId);
    }
}
