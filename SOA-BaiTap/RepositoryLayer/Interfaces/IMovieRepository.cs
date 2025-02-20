using SOA_BaiTap.CoreLayer.Entities;

namespace SOA_BaiTap.RepositoryLayer.Interfaces
{
    public interface IMovieRepository
    {
        public Task<List<Movie>> GetMoviesAsync();
        public Task<Movie?> GetMovieByIdAsync(int id);
        public Task AddMovieAsync(Movie movie);
        public Task UpdateMovieAsync(Movie movie);
        public Task DeleteMovieAsync(int movieId);
    }
}
