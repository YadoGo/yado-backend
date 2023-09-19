using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
using yado_backend.Models.Dtos;
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
        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReviewById(Guid reviewId)
        {
            var reviewDto = await _reviewRepository.GetReviewByIdAsync(reviewId);

            if (reviewDto == null)
            {
                return NotFound("Review not found");
            }

            return Ok(reviewDto);
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
        public async Task<IActionResult> InsertReview(ReviewCreateDto reviewCreateDto)
        {
            var success = await _reviewRepository.InsertReview(reviewCreateDto);
            if (success)
            {
                return Ok();
            }
            return BadRequest("Failed to insert review.");
        }


        [Authorize(Roles = "User")]
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(Guid reviewId, ReviewUpdateDto reviewDto)
        {
            var success = await _reviewRepository.UpdateReviewById(reviewId, reviewDto);
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
