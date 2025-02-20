using SOA_BaiTap.CoreLayer.Entities;

namespace SOA_BaiTap.RepositoryLayer.Interfaces
{
    public interface IMovieRepository
    {
        public Task<IEnumerable<Movie>> GetMoviesAsync();
        public Task AddMovieAsync(Movie movie);
        public Task UpdateMovieAsync(Movie movie);
        public Task DeleteMovieAsync(int movieId);
    }
}
