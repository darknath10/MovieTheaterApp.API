using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTheaterApp.API.Entities;
using MovieTheaterApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieTheaterApp.API.Services
{
    public class MovieTheaterRepository : IMovieTheaterRepository
    {
        private MovieTheaterContext _context;

        public MovieTheaterRepository(MovieTheaterContext context)
        {
            _context = context;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies.ToList();
        }

        public IEnumerable<Movie> SearchMovies(string term)
        {
            return _context.Movies.Where(m => m.Title.Contains(term));
        }

        public Movie GetMovie(int movieId)
        {
            return _context.Movies.Where(m => m.Id == movieId).FirstOrDefault();
        }

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
        }

        public IEnumerable<Review> GetReviews(int movieId)
        {
            return _context.Reviews.Where(r => r.MovieId == movieId).Include(r => r.User).ToList();
        }

        public Review GetReview(int movieId, User user)
        {
            return _context.Reviews.Where(r => r.MovieId == movieId && r.UserId == user.Id).FirstOrDefault();
        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
