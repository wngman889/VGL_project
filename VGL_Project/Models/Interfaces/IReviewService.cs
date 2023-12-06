namespace VGL_Project.Models.Interfaces
{
    public interface IReviewService
    {
        public Task<bool> AddReview(int authorId, int gameId, string reviewTitle, int rating, string reviewDescription);
        public Task<IEnumerable<ReviewRecommendation>?> GetReviews();

        public Task<ReviewRecommendation?> GetReview(int id);

        Task<bool> UpdateReview(int id, int newRating, string newReviewTitle, string newReviewDescription);
        Task<bool> DeleteReview(int id);
    }
}
