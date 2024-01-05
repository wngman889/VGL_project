using Microsoft.AspNetCore.Mvc;
using VGL_Project.Models;
using VGL_Project.Models.Interfaces;
using VGL_Project.Services;

namespace VGL_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService userService)
        {
            this._reviewService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(int authorId, int gameId, string reviewTitle, int rating, string reviewDescription)
        {
            await _reviewService.AddReview(authorId, gameId, reviewTitle, rating, reviewDescription);

            return Ok("Review Created");
        }
        [HttpGet("get-all-reviews")]
        public async Task<IActionResult> GetReviews()
        {
            return Ok(await _reviewService.GetReviews());
        }
        [HttpGet("get-review-by-id")]
        public async Task<IActionResult> GetReview(int id)
        {
            var result = await _reviewService.GetReview(id);

            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpPut("update-review/{id}")]
        public async Task<IActionResult> UpdateReview(int id, int newRating, string newReviewTitle, string newReviewDescription)
        {
            var isUpdated = await _reviewService.UpdateReview(id, newRating, newReviewTitle, newReviewDescription);

            if (!isUpdated) return NotFound();

            return Ok("Review Updated");
        }

        [HttpDelete("delete-review/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReview(id);

            if (result)
                return Ok(result);
            else
                return NotFound();
        }
    }
}
