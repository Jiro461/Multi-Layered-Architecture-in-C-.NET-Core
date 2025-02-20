using SOA_BaiTap.CommonLayer.Utilities;
using SOA_BaiTap.CoreLayer.DTO;
using SOA_BaiTap.CoreLayer.Entities;
using SOA_BaiTap.RepositoryLayer.Interfaces;

namespace SOA_BaiTap.ServiceLayer.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ITagRepository _tagRepository;
        public  MovieService (IMovieRepository movieRepository, ITagRepository tagRepository)
        {
            _movieRepository = movieRepository;
            _tagRepository = tagRepository;
        }
        //Crud Methods
        public async Task AddMovieAsync(MovieDTO moviedto)
        {
            var existingMovies = await _movieRepository.GetAllMoviesAsync();
            if (existingMovies.Any(m => m.Title == moviedto.Title))
            {
                throw new ArgumentException("A movie with the same title already exists.");
            }
            var movie = new Movie
            {
                Title = moviedto.Title,
                Genre = moviedto.Genre,
                ReleaseDate = moviedto.ReleaseDate.ToDateTime(),
                Description = moviedto.Description
            };

            var Tags = await _tagRepository.GetListTag(moviedto.Tags.ToList());

            Tags.ForEach(tag => movie.MovieSeriesTags.Add(new MovieSeriesTag
            {
                Movie = movie,
                Tag = tag,
            }));
            await _movieRepository.AddMovieAsync(movie);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> GetMovieByIdAsync (int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                throw new ArgumentException("Not Found Movie.");
            }
            return movie;
        }
        
    }
}
