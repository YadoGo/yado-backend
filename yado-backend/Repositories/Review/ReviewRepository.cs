using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models;
using yado_backend.Models.Dtos;

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

        public async Task<int> GetReviewCountByHotelId(Guid hotelId)
        {
            return await _dbContext.Reviews
                .Where(r => r.HotelId == hotelId)
                .CountAsync();
        }

        public async Task<string> GetAverageRatingByHotelId(Guid hotelId)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.HotelId == hotelId).ToListAsync();

            if (reviews.Any())
            {
                var averageRating = reviews.Average(r => r.Qualification);
                return averageRating.ToString(averageRating % 1 == 0 ? "0" : "0.0#");
            }
            else
            {
                return "0";
            }
        }


        public async Task<IEnumerable<Review>> GetAllReviewsByUserId(Guid userId)
        {
            return await _dbContext.Reviews.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<bool> InsertReview(ReviewCreateDto reviewCreateDto)
        {
            var user = await _dbContext.Users.FindAsync(reviewCreateDto.UserId);

            var hotel = await _dbContext.Hotels.FindAsync(reviewCreateDto.HotelId);

            if (user == null || hotel == null)
            {
                return false;
            }


            var review = new Review
            {
                Id = Guid.NewGuid(),
                Qualification = reviewCreateDto.Qualification,
                PositiveComment = reviewCreateDto.PositiveComment,
                NegativeComment = reviewCreateDto.NegativeComment,
                UserId = user.Id,
                HotelId = hotel.Id,
            };

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

        public async Task<bool> DeleteReviewById(Guid reviewId, Guid userId)
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
