using Xunit;
using Moq;
using SOA_BaiTap.RepositoryLayer.Interfaces;
using SOA_BaiTap.ServiceLayer.Services;
using SOA_BaiTap.CoreLayer.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using SOA_BaiTap.ServiceLayer.Services;
using SOA_BaiTap.CoreLayer.DTO;
using SOA_BaiTap.CommonLayer.Utilities;

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
            var movieDTO = new MovieDTO { Title = "Inception", Genre = "Sci-Fi", ReleaseDate = "26/04/2004", Description = "gr" };
            var movieEntity = new Movie { Title = "Inception", Genre = "Sci-Fi", ReleaseDate = "26/04/2004".ToDateTime(), Description = "gr" };

            _repositoryMock.Setup(repo => repo.GetMoviesAsync()).ReturnsAsync(new List<Movie>());
            _repositoryMock.Setup(repo => repo.AddMovieAsync(It.IsAny<Movie>())).Returns(Task.CompletedTask);

            // Act
            await _movieService.AddMovieAsync(movieDTO);

            // Assert
            _repositoryMock.Verify(repo => repo.AddMovieAsync(It.Is<Movie>(
                m => m.Title == movieDTO.Title && m.Genre == movieDTO.Genre
            )), Times.Once);
        }


        [Fact]
        public async Task AddMovieAsync_ShouldThrowException_WhenMovieTitleExists()
        {
            // Arrange
            var existingMovies = new List<Movie>
    {
        new Movie { Title = "Inception", Genre = "Sci-Fi" }
    };
            var newMovieDto = new MovieDTO { Title = "Inception", Genre = "Action" }; 

            _repositoryMock.Setup(repo => repo.GetMoviesAsync()).ReturnsAsync(existingMovies);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _movieService.AddMovieAsync(newMovieDto));
            Assert.Equal("A movie with the same title already exists.", exception.Message);

            _repositoryMock.Verify(repo => repo.AddMovieAsync(It.IsAny<Movie>()), Times.Never);
        }

    }
}
