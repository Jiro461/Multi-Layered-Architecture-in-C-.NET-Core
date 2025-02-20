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
        public async Task AddMovieAsync(Movie movie)
        {
            var existingMovies = await _movieRepository.GetAllMoviesAsync();
            if (existingMovies.Any(m => m.Title == movie.Title))
            {
                throw new ArgumentException("A movie with the same title already exists.");
            }
            await _movieRepository.AddMovieAsync(movie);
        }

        // Stored Procedure Method
        public async Task<IEnumerable<Movie>> GetTopRatedMoviesWithSpAsync(int topCount)
        {
            try
            {
                return await
               _movieRepository.GetTopRatedMoviesWithSpAsync(topCount);
            }
            catch (Exception ex)
            {
                // Log error, provide a generic message, or rethrow
                throw new ApplicationException("An error occurred while retrieving top - rated movies.", ex);
            }
        }

    }
}
