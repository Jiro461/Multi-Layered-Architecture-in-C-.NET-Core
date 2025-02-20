using Xunit;
using Moq;
using SOA_BaiTap.RepositoryLayer;
using SOA_BaiTap.CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
using SOA_BaiTap.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOA_BaiTap.CommonLayer.Utilities;

namespace MovieSeries.Tests.Repositories
{
    public class MovieRepositoryTests
    {
        private readonly MovieRepository _movieRepository;
        private readonly AppDbContext _dbContext;
        private readonly Mock<DbSet<Movie>> _dbSetMock;
        private readonly List<Movie> _movies;

        public MovieRepositoryTests()
        {
            // 🔹 Cấu hình DbContextOptions
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=DESKTOP-DVFH4C1\\SQLEXPRESS;Database=MoviesAppDB;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            // 🔹 Khởi tạo AppDbContext với provider SQL Server
            _dbContext = new AppDbContext(options);

            // 🔹 Danh sách giả lập dữ liệu
            _movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi" },
                new Movie { Id = 2, Title = "The Matrix", Genre = "Sci-Fi" }
            };

            // 🔹 Mock DbSet<Movie>
            _dbSetMock = new Mock<DbSet<Movie>>();

            var movieQueryable = _movies.AsQueryable();
            _dbSetMock.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(movieQueryable.Provider);
            _dbSetMock.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(movieQueryable.Expression);
            _dbSetMock.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(movieQueryable.ElementType);
            _dbSetMock.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(movieQueryable.GetEnumerator());

            // 🔹 Mock FindAsync()
            _dbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
            {
                int id = (int)ids[0];
                return _movies.FirstOrDefault(m => m.Id == id);
            });

            // 🔹 Mock AddAsync()
            _dbSetMock.Setup(m => m.AddAsync(It.IsAny<Movie>(), default)).Callback<Movie, CancellationToken>((m, _) => _movies.Add(m));

            // 🔹 Khởi tạo repository với AppDbContext
            _movieRepository = new MovieRepository(_dbContext);
        }

        // ✅ Kiểm thử thêm phim
        [Fact]
        public async Task AddMovieAsync_ShouldCallAddAndSaveChanges()
        {
            // Arrange
            var movie = new Movie { Title = "Interstellar", Genre = "Sci-Fi", ReleaseDate = "26/04/2004".ToDateTime(), Description = "gr" };

            // Act
            await _movieRepository.AddMovieAsync(movie);


            // Assert
            var result = await _dbContext.Movies.FindAsync(movie.Id);
            Assert.NotNull(result);
        }

        // ✅ Kiểm thử lấy danh sách phim
        [Fact]
        public async Task GetMoviesAsync_ShouldReturnListOfMovies()
        {
            // Act
            var result = await _movieRepository.GetMoviesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }

        // ✅ Kiểm thử cập nhật phim
        [Fact]
        public async Task UpdateMovieAsync_ShouldCallUpdateAndSaveChanges()
        {
            // Arrange
            var movie = new Movie { Id = 2, Title = "Inception - Updated", Genre = "Sci-Fi", ReleaseDate = "26/04/2004".ToDateTime(), Description = "gr" };


            // Act
            await _movieRepository.UpdateMovieAsync(movie);

            // Assert
            var updatedMovie = await _dbContext.Movies.FindAsync(movie.Id);
            Assert.Equal("Inception - Updated", updatedMovie.Title);
        }

        // ✅ Kiểm thử xóa phim khi phim tồn tại
        [Fact]
        public async Task DeleteMovieAsync_ShouldCallRemoveAndSaveChanges_WhenMovieExists()
        {
            // Arrange
            var movie = _movies.FirstOrDefault(m => m.Id == 1);

            // Act
            await _movieRepository.DeleteMovieAsync(1);

            // Assert
            var result = await _dbContext.Movies.FindAsync(1);
            Assert.Null(result);
        }

        // ❌ Kiểm thử xóa phim khi phim không tồn tại
        [Fact]
        public async Task DeleteMovieAsync_ShouldNotCallRemove_WhenMovieDoesNotExist()
        {
            // Arrange
            int nonExistentMovieId = 99;

            // Act
            await _movieRepository.DeleteMovieAsync(nonExistentMovieId);

            // Assert
            var result = await _dbContext.Movies.FindAsync(nonExistentMovieId);
            Assert.Null(result);
        }
    }
}
