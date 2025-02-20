using Xunit;
using Moq;
using SOA_BaiTap.RepositoryLayer.Interfaces;
using SOA_BaiTap.ServiceLayer.Services;
using SOA_BaiTap.CoreLayer.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using SOA_BaiTap.ServiceLayer.Services;

namespace MovieSeries.Tests.Services
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _repositoryMock;
        private readonly Mock<ITagRepository> _repositoryTagMock;
        private readonly MovieService _movieService;

        public MovieServiceTests()
        {
            _repositoryMock = new Mock<IMovieRepository>();
            _repositoryTagMock = new Mock<ITagRepository>();
            _movieService = new MovieService(_repositoryMock.Object, _repositoryTagMock.Object);
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
    }
}
