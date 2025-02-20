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
        public async Task AddMovieAsync_ShouldCallRepositoryAddAsync()
        {
            // Arrange
            var movie = new Movie { Title = "Inception", Genre = "Sci-Fi" };
            _repositoryMock.Setup(repo => repo.AddAsync(movie)).Returns(Task.CompletedTask);

            // Act
            await _movieService.AddMovieAsync(movie);

            // Assert
            _repositoryMock.Verify(repo => repo.AddAsync(movie), Times.Once);
        }
    }
}
