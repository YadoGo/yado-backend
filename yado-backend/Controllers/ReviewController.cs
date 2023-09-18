using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using yado_backend.Models;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [AllowAnonymous]
        [HttpGet("hotel/{hotelId}")]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetAllReviewsByHotelId(Guid hotelId)
        {
            var reviews = await _reviewRepository.GetAllReviewsByHotelId(hotelId);
            return Ok(reviews);
        }

        [AllowAnonymous]
        [HttpGet("hotel/{hotelId}/review-count")]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetReviewCountByHotelId(Guid hotelId)
        {
            try
            {
                var reviewCount = await _reviewRepository.GetReviewCountByHotelId(hotelId);
                return Ok(reviewCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet("hotel/{hotelId}/average-rating")]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetAverageRatingByHotelId(Guid hotelId)
        {
            try
            {
                var averageRating = await _reviewRepository.GetAverageRatingByHotelId(hotelId);
                return Ok(averageRating);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet("user/{userId}")]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetAllReviewsByUserId(Guid userId)
        {
            var reviews = await _reviewRepository.GetAllReviewsByUserId(userId);
            return Ok(reviews);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> InsertReview(Review review)
        {
            var success = await _reviewRepository.InsertReview(review);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public async Task<IActionResult> UpdateReview(Review review)
        {
            var success = await _reviewRepository.UpdateReviewById(review);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }

        [Authorize(Roles = "User, Admin")]
        [HttpDelete("{reviewId}/{userId}")]
        public async Task<IActionResult> DeleteReview(Guid reviewId, Guid userId)
        {

            var success = await _reviewRepository.DeleteReviewById(reviewId, userId);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
