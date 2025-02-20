using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SOA_BaiTap.Controllers;
using SOA_BaiTap.ServiceLayer.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using SOA_BaiTap.CoreLayer.Entities;

namespace MovieSeries.Tests.Controllers
{
    public class MovieControllerTests
    {
        private readonly Mock<IMovieService> _serviceMock;
        private readonly  MovieController _controller;

        public MovieControllerTests()
        {
            _serviceMock = new Mock<IMovieService>();
            _controller = new MovieController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetMovies_ReturnsOkResult_WithListOfMovies()
        {
            // Arrange
            var movies = new List<Movie> { new Movie { Title = "Inception" }, new Movie { Title = "The Matrix" } };
            _serviceMock.Setup(s => s.GetAllMoviesAsync()).ReturnsAsync(movies);

            // Act
            var result = await _controller.GetMovies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(movies, okResult.Value);
        }
    }
}
