using Microsoft.EntityFrameworkCore;
using VGL_Project.Data;
using VGL_Project.Models;
using VGL_Project.Models.Interfaces;

namespace VGL_Project.Services
{
    public class ReviewService : IReviewService
    {
        private readonly VGLDbContext _dbContext;

        private readonly ILogger<ReviewService> _logger;

        public ReviewService(VGLDbContext dbContext, ILogger<ReviewService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<bool> AddReview(int authorId, int gameId, string reviewTitle, int rating, string reviewDescription)
        {
            try
            {
                var author = await _dbContext.Users.FindAsync(authorId);

                if (author == null)
                {
                    _logger.LogError($"User with ID {authorId} not found.");
                    return false;
                }
                var game = await _dbContext.Games.FindAsync(gameId);

                if (game == null)
                {
                    _logger.LogError($"Game with ID {gameId} not found.");
                    return false;
                }
                var newReview = new ReviewRecommendation
                {
                    AuthorId = authorId,
                    Author = author,
                    GameId = gameId,
                    Game = game,
                    ReviewTitle = reviewTitle,
                    Rating = rating,
                    ReviewDescription = reviewDescription
                };

                _dbContext.ReviewRecommendations.Add(newReview);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task<IEnumerable<ReviewRecommendation>?> GetReviews()
        {
            try
            {
                var reviews = await _dbContext.ReviewRecommendations.ToListAsync();
                return reviews;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<ReviewRecommendation?> GetReview(int id)
        {
            try
            {
                var review = await _dbContext.ReviewRecommendations.FindAsync(id);
                return review;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<bool> UpdateReview(int id, int newRating, string newReviewTitle, string newReviewDescription)
        {
            try
            {
                var existingReview = await _dbContext.ReviewRecommendations.FindAsync(id);

                if (existingReview == null)
                    return false;

                existingReview.Rating = newRating;
                existingReview.ReviewTitle = newReviewTitle;
                existingReview.ReviewDescription = newReviewDescription;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteReview(int id)
        {
            try
            {
                var reviewToDelete = await _dbContext.ReviewRecommendations.FindAsync(id);

                if (reviewToDelete == null)
                    return false;

                _dbContext.ReviewRecommendations.Remove(reviewToDelete);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
