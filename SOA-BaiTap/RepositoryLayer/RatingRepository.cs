using Microsoft.EntityFrameworkCore;
using SOA_BaiTap.CoreLayer.Entities;
using SOA_BaiTap.DAL;
using SOA_BaiTap.RepositoryLayer.Interfaces;

namespace SOA_BaiTap.RepositoryLayer
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _context;

        public RatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            return await _context.Ratings.ToListAsync();
        }

        public async Task<Rating> GetByIdAsync(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        public async Task AddAsync(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rating = await GetByIdAsync(id);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> GetAverageRatingAsync(int movieId)
        {
            var ratings = await _context.Ratings.Where(r => r.MovieId == movieId).ToListAsync();
            return ratings.Any() ? ratings.Average(r => r.Value) : 0;
        }
    }
}
