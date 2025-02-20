using Xunit;
using Moq;
using SOA_BaiTap.RepositoryLayer.Interfaces;
using SOA_BaiTap.CoreLayer.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using SOA_BaiTap.RepositoryLayer;
using Microsoft.EntityFrameworkCore;
using SOA_BaiTap.DAL;

namespace MovieSeries.Tests.Repositories
{
    public class MovieRepositoryTests
    {
        private readonly MovieRepository _movieRepository;
        private readonly Mock<AppDbContext> _dbContextMock;
        private readonly Mock<DbSet<Movie>> _dbSetMock;

        public MovieRepositoryTests()
        {
            _dbContextMock = new Mock<AppDbContext>();
            _dbSetMock = new Mock<DbSet<Movie>>();
            _dbContextMock.Setup(db => db.Movies).Returns(_dbSetMock.Object);
            _movieRepository = new MovieRepository(_dbContextMock.Object);
        }

        [Fact]
        public async Task AddMovieAsync_ShouldCallAddAndSaveChanges()
        {
            // Arrange
            var movie = new Movie { Title = "Inception", Genre = "Sci-Fi" };

            // Act
            await _movieRepository.AddMovieAsync(movie);

            // Assert
            _dbSetMock.Verify(db => db.AddAsync(movie, default), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetMoviesAsync_ShouldReturnListOfMovies()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie { Title = "Inception", Genre = "Sci-Fi" },
                new Movie { Title = "The Matrix", Genre = "Sci-Fi" }
            };
            var mockDbSet = new Mock<DbSet<Movie>>();
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(movies.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(movies.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(movies.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(movies.AsQueryable().GetEnumerator());

            _dbContextMock.Setup(db => db.Movies).Returns(mockDbSet.Object);

            // Act
            var result = await _movieRepository.GetMoviesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateMovieAsync_ShouldCallUpdateAndSaveChanges()
        {
            // Arrange
            var movie = new Movie { Id = 1, Title = "Interstellar", Genre = "Sci-Fi" };

            // Act
            await _movieRepository.UpdateMovieAsync(movie);

            // Assert
            _dbSetMock.Verify(db => db.Update(movie), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteMovieAsync_ShouldCallRemoveAndSaveChanges_WhenMovieExists()
        {
            // Arrange
            var movie = new Movie { Id = 1, Title = "Interstellar", Genre = "Sci-Fi" };
            _dbSetMock.Setup(db => db.FindAsync(1)).ReturnsAsync(movie);

            // Act
            await _movieRepository.DeleteMovieAsync(1);

            // Assert
            _dbSetMock.Verify(db => db.Remove(movie), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteMovieAsync_ShouldNotCallRemove_WhenMovieDoesNotExist()
        {
            // Arrange
            _dbSetMock.Setup(db => db.FindAsync(1)).ReturnsAsync((Movie)null);

            // Act
            await _movieRepository.DeleteMovieAsync(1);

            // Assert
            _dbSetMock.Verify(db => db.Remove(It.IsAny<Movie>()), Times.Never);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Never);
        }

    }
}

