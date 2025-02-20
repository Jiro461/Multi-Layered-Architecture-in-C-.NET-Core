using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOA_BaiTap.CoreLayer.DTO;
using SOA_BaiTap.CoreLayer.Entities;
using SOA_BaiTap.ServiceLayer.Services;

namespace SOA_BaiTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController (IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }
        [HttpGet("/all")]
        public async Task<IActionResult> GetAllMovies()
        {
            return Ok(await _movieService.GetMoviesAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CreateMovie (MovieDTO movie)
        {
            await _movieService.AddMovieAsync(movie);
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie (int id, MovieDTO movie)
        {
            var result = await _movieService.UpdateMovie(id, movie);
            if(result == null) return NotFound();
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (await _movieService.DeleteMovie(id))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
