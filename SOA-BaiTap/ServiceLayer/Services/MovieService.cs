﻿using Microsoft.AspNetCore.Mvc;
using SOA_BaiTap.CommonLayer.Utilities;
using SOA_BaiTap.CoreLayer.DTO;
using SOA_BaiTap.RepositoryLayer.Interfaces;
using SOA_BaiTap.ServiceLayer.Services;

namespace SOA_BaiTap.CoreLayer.Entities
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ITagRepository _tagRepository;
        public MovieService(IMovieRepository movieRepository, ITagRepository tagRepository)
        {
            _movieRepository = movieRepository;
            _tagRepository = tagRepository;
        }
        //Crud Methods
        public async Task AddMovieAsync(MovieDTO moviedto)
        {
            var existingMovies = await _movieRepository.GetMoviesAsync();
            if (existingMovies.Any(m => m.Title == moviedto.Title))
            {
                throw new ArgumentException("A movie with the same title already exists.");
            }
            var movie = new Movie
            {
                Title = moviedto.Title,
                Genre = moviedto.Genre,
                ReleaseDate = moviedto.ReleaseDate.ToDateTime(),
                Description = moviedto.Description
            };

            var Tags = await _tagRepository.GetListTag(moviedto.Tags.ToList());
            if (Tags != null && Tags.Any())
            {
                Tags.ForEach(tag => movie.MovieSeriesTags.Add(new MovieSeriesTag
                {
                    Movie = movie,
                    Tag = tag,
                }));
            }
            await _movieRepository.AddMovieAsync(movie);
        }
        public async Task<MovieGetDTO> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                throw new ArgumentException("Not Found Movie.");
            }
            return new MovieGetDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate.FormatDate(),
                Description = movie.Description,
                Tags = movie.MovieSeriesTags.Select(mst => mst.Tag.Name).ToList()
            };
        }

        public async Task<List<MovieGetDTO>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            var data = movies.Select(movie => new MovieGetDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate.FormatDate(),
                Description = movie.Description,
                Tags = movie.MovieSeriesTags.Select(mst => mst.Tag.Name).ToList()
            });
            return data.ToList();
        }

        public async Task<MovieDTO?> UpdateMovie(int id, MovieDTO moviedto)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null) return null;
            movie.Title = moviedto.Title;
            movie.Description = moviedto.Description;
            movie.Genre = moviedto.Genre;
            movie.ReleaseDate = moviedto.ReleaseDate.ToDateTime();
            await _movieRepository.UpdateMovieAsync(movie);
            return moviedto;
        }

        public async Task<bool> DeleteMovie (int id)
        {
            return await _movieRepository.DeleteMovieAsync(id);
       
        }
    }
}
