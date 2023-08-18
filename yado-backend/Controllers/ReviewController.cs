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

        [HttpGet("hotel/{hotelUuid}")]
        public async Task<IActionResult> GetAllReviewsByHotelUuid(string hotelUuid)
        {
            var reviews = await _reviewRepository.GetAllReviewsByHotelUuid(hotelUuid);
            return Ok(reviews);
        }

        [HttpGet("user/{userUuid}")]
        public async Task<IActionResult> GetAllReviewsByUserUuid(string userUuid)
        {
            var reviews = await _reviewRepository.GetAllReviewsByUserUuid(userUuid);
            return Ok(reviews);
        }

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

        [HttpDelete("{reviewId}/{userUuid}")]
        public async Task<IActionResult> DeleteReview(int reviewId, string userUuid)
        {
            var success = await _reviewRepository.DeleteReviewById(reviewId, userUuid);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
