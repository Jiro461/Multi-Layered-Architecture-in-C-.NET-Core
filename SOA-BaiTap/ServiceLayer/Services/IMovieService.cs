using SOA_BaiTap.CoreLayer.DTO;
using SOA_BaiTap.CoreLayer.Entities;

namespace SOA_BaiTap.ServiceLayer.Services
{
    public interface IMovieService
    {
        public Task AddMovieAsync(MovieDTO movie);
        public Task<List<MovieGetDTO>> GetMoviesAsync();
        public Task<Movie> GetMovieByIdAsync(int id);
        public Task<Movie> UpdateMovie (int id,  MovieDTO movie);
    }
}
