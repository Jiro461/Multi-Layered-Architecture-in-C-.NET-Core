using SOA_BaiTap.CoreLayer.Entities;
using SOA_BaiTap.RepositoryLayer.Interfaces;

namespace SOA_BaiTap.ServiceLayer.Services
{
    public class MovieService
    {
        private readonly IMovieRepository _movieRepository;
        public  MovieService (IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        //Crud Methods
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllMoviesAsync();
        }
        public async Task AddMovieAsync(Movie movie)
        {
            await _movieRepository.AddMovieAsync(movie);
        }
        // Stored Procedure Method
        public async Task<IEnumerable<Movie>> GetTopRatedMoviesWithSpAsync(int topCount)
        {
            return await
           _movieRepository.GetTopRatedMoviesWithSpAsync(topCount);
        }

    }
}
