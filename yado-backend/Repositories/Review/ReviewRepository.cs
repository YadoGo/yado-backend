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

        public async Task<IEnumerable<Review>> GetAllReviewsByHotelUuid(string hotelUuid)
        {
            return await _dbContext.Reviews.Where(r => r.HotelUuid == hotelUuid).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByUserUuid(string userUuid)
        {
            return await _dbContext.Reviews.Where(r => r.UserId == userUuid).ToListAsync();
        }

        public async Task<bool> InsertReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateReviewById(Review review)
        {
            var existingReview = await _dbContext.Reviews.FindAsync(review.ID);
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

        public async Task<bool> DeleteReviewById(int reviewId, string userUuid)
        {
            var review = await _dbContext.Reviews.FirstOrDefaultAsync(r =>
                r.ID == reviewId && r.UserId == userUuid);

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
