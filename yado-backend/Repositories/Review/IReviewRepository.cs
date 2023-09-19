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
        Task<ReviewUpdateDto> GetReviewByIdAsync(Guid reviewId);
        Task<bool> InsertReview(ReviewCreateDto reviewCreateDto);
        Task<bool> UpdateReviewById(Guid reviewId, ReviewUpdateDto reviewDto);
        Task<bool> DeleteReviewById(Guid reviewId, Guid userId);
    }
}
