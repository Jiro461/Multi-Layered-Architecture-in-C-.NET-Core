using SOA_BaiTap.CoreLayer.DTO;
using SOA_BaiTap.CoreLayer.Entities;

namespace SOA_BaiTap.ServiceLayer.Services
{
    public interface IMovieService
    {
        public Task AddMovieAsync(MovieDTO movie);
        public Task<List<MovieGetDTO>> GetMoviesAsync();
        public Task<MovieGetDTO> GetMovieByIdAsync(int id);
        public Task<MovieDTO?> UpdateMovie (int id,  MovieDTO movie);
        public Task<bool> DeleteMovie(int id);
    }
}
