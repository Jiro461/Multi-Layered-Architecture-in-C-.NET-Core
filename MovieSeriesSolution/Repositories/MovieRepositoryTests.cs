using Xunit;
using Moq;
using SOA_BaiTap.RepositoryLayer.Interfaces;
using SOA_BaiTap.CoreLayer.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MovieSeries.Tests.Repositories
{
    public class MovieRepositoryTests
    {
        private readonly Mock<IMovieRepository> _repositoryMock;

        public MovieRepositoryTests()
        {
            _repositoryMock = new Mock<IMovieRepository>();
        }

        [Fact]
        public async Task AddAsync_ShouldCallAddAsync_WhenMovieIsValid()
        {
            // Arrange
            var movie = new Movie { Title = "Inception", Genre = "Sci-Fi" };
            _repositoryMock.Setup(repo => repo.AddMovieAsync(movie)).Returns(Task.CompletedTask);

            // Act
            await _repositoryMock.Object.AddMovieAsync(movie);

            // Assert
            _repositoryMock.Verify(repo => repo.AddMovieAsync(movie), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfMovies()
        {
            // Arrange
            var movies = new List<Movie> { new Movie { Title = "Inception" }, new Movie { Title = "The Matrix" } };
            _repositoryMock.Setup(repo => repo.GetMoviesAsync()).ReturnsAsync(movies);

            // Act
            var result = await _repositoryMock.Object.GetMoviesAsync();

            // Assert
            Assert.Equal(movies, result);
        }
    }
}
