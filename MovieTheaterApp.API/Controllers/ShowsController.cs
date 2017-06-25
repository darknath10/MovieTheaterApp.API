using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class ShowsController : Controller
    {
        private IMovieTheaterRepository _movieTheaterRepository;

        public ShowsController(IMovieTheaterRepository movieTheaterRepository)
        {
            _movieTheaterRepository = movieTheaterRepository;
        }

        [HttpPost("new")]
        public IActionResult AddShow([FromBody] ShowDtoAdd showDto)
        {
            try
            {
                if (showDto == null) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var movie = _movieTheaterRepository.GetMovie(showDto.MovieId, false);
                var hall = _movieTheaterRepository.GetHall(showDto.HallId);

                if (movie == null || hall == null) return NotFound();

                var show = new Show() { Date = showDto.Date, HallId = showDto.HallId, MovieId = showDto.MovieId };

                _movieTheaterRepository.AddShow(show);

                if (!_movieTheaterRepository.Save()) return StatusCode(500, "An error occured while adding the show.");

                var addedShowDto = Mapper.Map<ShowDto>(show);
                return Ok(addedShowDto);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
