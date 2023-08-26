using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _dbContext;

        public ReviewRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByHotelId(Guid hotelId)
        {
            return await _dbContext.Reviews.Where(r => r.HotelId == hotelId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByUserId(Guid userId)
        {
            return await _dbContext.Reviews.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<bool> InsertReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateReviewById(Review review)
        {
            var existingReview = await _dbContext.Reviews.FindAsync(review.Id);
            if (existingReview != null)
            {
                existingReview.Qualification = review.Qualification;
                existingReview.PositiveComment = review.PositiveComment;
                existingReview.NegativeComment = review.NegativeComment;

                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<bool> DeleteReviewById(int reviewId, Guid userId)
        {
            var review = await _dbContext.Reviews.FirstOrDefaultAsync(r =>
                r.Id == reviewId && r.UserId == userId);

            if (review != null)
            {
                _dbContext.Reviews.Remove(review);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
