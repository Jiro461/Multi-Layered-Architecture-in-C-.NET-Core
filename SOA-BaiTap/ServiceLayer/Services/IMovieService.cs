using SOA_BaiTap.CoreLayer.DTO;
using SOA_BaiTap.CoreLayer.Entities;

namespace SOA_BaiTap.ServiceLayer.Services
{
    public interface IMovieService
    {
        public Task AddMovieAsync(MovieDTO movie);
        public Task<Movie> GetMovieByIdAsync(int id);
    }
}
