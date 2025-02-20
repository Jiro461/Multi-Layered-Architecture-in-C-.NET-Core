using SOA_BaiTap.CoreLayer.Entities;

namespace SOA_BaiTap.ServiceLayer.Services
{
    public interface IReviewService
    {
        public  Task<IEnumerable<Review>> GetAllReviewsAsync()  ;

        public Task<Review> GetReviewByIdAsync(int id);

        public Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(int movieId);

        public Task AddReviewAsync(Review review);

        public Task UpdateReviewAsync(Review review);

        public Task DeleteReviewAsync(int id);
    }
}
