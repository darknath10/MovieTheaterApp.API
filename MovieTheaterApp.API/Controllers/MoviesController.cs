using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieTheaterApp.API.Entities;
using MovieTheaterApp.API.Models;
using MovieTheaterApp.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowMyApp")]
    [Authorize("SuperUsers")]
    public class MoviesController : Controller
    {
        private IMovieTheaterRepository _movieTheaterRepository;

        public MoviesController(IMovieTheaterRepository movieTheaterRepository)
        {
            _movieTheaterRepository = movieTheaterRepository;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetMovies()
        {
            try
            {
                var movies = _movieTheaterRepository.GetMovies();
                var moviesDto = Mapper.Map<IEnumerable<MovieDto>>(movies);
                return Ok(moviesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetMovie")]
        [AllowAnonymous]
        public IActionResult GetMovie(int id)
        {
            //var movie = MoviesDataStore.Current.Movies.FirstOrDefault(m => m.Id == id);
            //if (movie == null)
            //{
            //    return NotFound();
            //}
            //return Ok(movie);

            try
            {
                var movie = _movieTheaterRepository.GetMovie(id);
                if (movie == null) return NotFound();

                return Ok(Mapper.Map<MovieDto>(movie));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search/{term}")]
        [AllowAnonymous]
        public IActionResult SearchMovies(string term)
        {
            try
            {
                if (term == null) return BadRequest("Search term cannot be null");

                var movies = _movieTheaterRepository.SearchMovies(term);
                if (movies == null) return NotFound();
                return Ok(Mapper.Map<IEnumerable<MovieDto>>(movies));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] MovieDtoAdd movieDto)
        {
            try
            {
                if (movieDto == null) return BadRequest();

                if (!ModelState.IsValid) return BadRequest(ModelState);

                if (_movieTheaterRepository.GetMovies()
                    .Where(m => (m.Title == movieDto.Title && m.Release_date == movieDto.Release_date)).Any())
                {
                    return StatusCode(409, "This movie is already in the catalog.");
                }

                var movie = Mapper.Map<Movie>(movieDto);

                _movieTheaterRepository.AddMovie(movie);

                if (!_movieTheaterRepository.Save()) return StatusCode(500, "An error occured while saving the movie.");

                var createdMovieDto = Mapper.Map<MovieDto>(movie);

                return CreatedAtRoute("GetMovie", new { id = createdMovieDto.Id }, createdMovieDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{movieId}")]
        public IActionResult EditMovie(int movieId, [FromBody] MovieDtoAdd movieDto)
        {
            try
            {
                if (movieDto == null) return BadRequest();

                if (!ModelState.IsValid) return BadRequest(ModelState);

                var movie = _movieTheaterRepository.GetMovie(movieId);

                if (movie == null) return NotFound();

                Mapper.Map(movieDto, movie);

                if (!_movieTheaterRepository.Save()) return StatusCode(500, "An error occured while updating the movie.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{movieId}")]
        public IActionResult DeleteMovie(int movieId)
        {
            try
            {
                var movie = _movieTheaterRepository.GetMovie(movieId);
                if (movie == null) return NotFound();

                _movieTheaterRepository.DeleteMovie(movie);

                if (!_movieTheaterRepository.Save()) return StatusCode(500, "An error occured while deleting the movie.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
