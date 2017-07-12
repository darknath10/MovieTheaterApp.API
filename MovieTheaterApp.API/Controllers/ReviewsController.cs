using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
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
    [EnableCors("AllowMyApp")]
    [Route("api/movies/{movieId}/[controller]")]
    [Authorize]
    public class ReviewsController : Controller
    {
        private UserManager<User> _userManager;
        private IMovieTheaterRepository _mtRepo;

        public ReviewsController(UserManager<User> userManager, IMovieTheaterRepository mtRepo)
        {
            _userManager = userManager;
            _mtRepo = mtRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetReviews(int movieId)
        {
            try
            {
                var movie = _mtRepo.GetMovie(movieId);
                if (movie == null) return BadRequest();

                var reviews = _mtRepo.GetReviews(movieId);
                var reviewsDto = Mapper.Map<ICollection<ReviewDto>>(reviews);
                return Ok(reviewsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{userName}", Name = "GetReview")]
        public async Task<IActionResult> GetReview(int movieId, string userName)
        {
            try
            {
                var movie = _mtRepo.GetMovie(movieId);
                if (movie == null) return BadRequest();
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null) return BadRequest();

                var review = _mtRepo.GetReview(movieId, user);
                return Ok(Mapper.Map<ReviewDto>(review));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost("new")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDtoAdd reviewDto)
        {
            try
            {
                if (reviewDto == null) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = await _userManager.FindByNameAsync(reviewDto.UserName);
                if (user == null) return BadRequest();

                var movie = _mtRepo.GetMovie(reviewDto.MovieId);
                if (movie == null) return BadRequest();

                var review = new Review()
                {
                    UserId = user.Id,
                    MovieId = reviewDto.MovieId,
                    Score = reviewDto.Score,
                    Text = reviewDto.Text
                };

                _mtRepo.AddReview(review);

                if (!_mtRepo.Save()) return StatusCode(500, "An error occured while saving the review.");

                var newReviewDto = Mapper.Map<ReviewDto>(review);

                return Ok(newReviewDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditReview(int movieId, [FromBody] ReviewDtoAdd reviewDto)
        {
            try
            {
                if (reviewDto == null) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = await _userManager.FindByNameAsync(reviewDto.UserName);
                if (user == null) return BadRequest();

                var review = _mtRepo.GetReview(movieId, user);
                if (review == null) return NotFound();

                Mapper.Map(reviewDto, review);

                if (!_mtRepo.Save()) return StatusCode(500, "An error occured while editing the review.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
