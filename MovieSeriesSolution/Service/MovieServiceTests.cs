using Xunit;
using Moq;
using SOA_BaiTap.RepositoryLayer.Interfaces;
using SOA_BaiTap.ServiceLayer.Services;
using SOA_BaiTap.CoreLayer.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MovieSeries.Tests.Services
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _repositoryMock;
        private readonly MovieService _movieService;

        public MovieServiceTests()
        {
            _repositoryMock = new Mock<IMovieRepository>();
            _movieService = new MovieService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddMovieAsync_ShouldCallRepositoryAddAsync_WhenMovieIsUnique()
        {
            // Arrange
            var movie = new Movie { Title = "Inception", Genre = "Sci-Fi" };
            _repositoryMock.Setup(repo => repo.GetAllMoviesAsync()).ReturnsAsync(new List<Movie>());
            _repositoryMock.Setup(repo => repo.AddMovieAsync(movie)).Returns(Task.CompletedTask);

            // Act
            await _movieService.AddMovieAsync(movie);

            // Assert
            _repositoryMock.Verify(repo => repo.AddMovieAsync(movie), Times.Once);
        }

        [Fact]
        public async Task AddMovieAsync_ShouldThrowException_WhenMovieTitleExists()
        {
            // Arrange
            var existingMovies = new List<Movie>
            {
                new Movie { Title = "Inception", Genre = "Sci-Fi" }
            };
            var newMovie = new Movie { Title = "Inception", Genre = "Action" };

            _repositoryMock.Setup(repo => repo.GetAllMoviesAsync()).ReturnsAsync(existingMovies);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _movieService.AddMovieAsync(newMovie));
            Assert.Equal("A movie with the same title already exists.", exception.Message);

            _repositoryMock.Verify(repo => repo.AddMovieAsync(It.IsAny<Movie>()), Times.Never);
        }

        [Fact]
        public async Task GetTopRatedMoviesWithSpAsync_ShouldReturnMovies_WhenStoredProcedureSucceeds()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie { Title = "Inception", Genre = "Sci-Fi" },
                new Movie { Title = "The Dark Knight", Genre = "Action" }
            };
            _repositoryMock.Setup(repo => repo.GetTopRatedMoviesWithSpAsync(2)).ReturnsAsync(movies);

            // Act
            var result = await _movieService.GetTopRatedMoviesWithSpAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetTopRatedMoviesWithSpAsync_ShouldThrowApplicationException_WhenStoredProcedureFails()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetTopRatedMoviesWithSpAsync(2)).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationException>(() => _movieService.GetTopRatedMoviesWithSpAsync(2));
            Assert.Equal("An error occurred while retrieving top - rated movies.", exception.Message);
        }


    }
}
