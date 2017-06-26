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

        public Movie GetMovie(int movieId, bool includeShows)
        {
            if (includeShows)
            {
                return _context.Movies.Include(m => m.Shows).ThenInclude(s => s.Hall).Where(m => m.Id == movieId).FirstOrDefault();
            }

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

        public Hall GetHall(int hallId)
        {
            return _context.Halls.Where(h => h.Id == hallId).FirstOrDefault();
        }

        public void AddShow(Show show)
        {
            _context.Shows.Add(show);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
