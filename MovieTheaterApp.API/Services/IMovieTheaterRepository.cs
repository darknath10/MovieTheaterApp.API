using MovieTheaterApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Services
{
    public interface IMovieTheaterRepository
    {
        IEnumerable<Movie> GetMovies();

        IEnumerable<Movie> SearchMovies(string term);

        Movie GetMovie(int movieId);

        void AddMovie(Movie movie);

        void DeleteMovie(Movie movie);

        bool Save();
    }
}
