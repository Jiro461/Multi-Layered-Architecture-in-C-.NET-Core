using SOA_BaiTap.CoreLayer.Entities;
using SOA_BaiTap.RepositoryLayer;
using SOA_BaiTap.RepositoryLayer.Interfaces;
using SOA_BaiTap.ServiceLayer.Services;

namespace SOA_BaiTap.ServiceLayer.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync() => await _reviewRepository.GetAllAsync();

        public async Task<Review> GetReviewByIdAsync(int id) => await _reviewRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(int movieId) => await _reviewRepository.GetReviewsByMovieIdAsync(movieId);

        public async Task AddReviewAsync(Review review) => await _reviewRepository.AddAsync(review);

        public async Task UpdateReviewAsync(Review review) => await _reviewRepository.UpdateAsync(review);

        public async Task DeleteReviewAsync(int id) => await _reviewRepository.DeleteAsync(id);
    }
}
