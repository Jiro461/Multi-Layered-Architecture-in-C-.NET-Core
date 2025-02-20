using Microsoft.EntityFrameworkCore;
using SOA_BaiTap.CoreLayer.Entities;
using SOA_BaiTap.DAL;
using SOA_BaiTap.RepositoryLayer.Interfaces;

namespace SOA_BaiTap.RepositoryLayer
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        public MovieRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }


        public async Task AddMovieAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync (int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null) return; 
            _context.Movies.Remove(movie);
        }
    }
}
