using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Models
{
    public class MovieDto
    {
        public int Id { get; set; }

        public string Backdrop_path { get; set; }

        public int Tmdb_id { get; set; }

        public string Overview { get; set; }

        public decimal Popularity { get; set; }

        public string Poster_path { get; set; }

        public string Release_date { get; set; }

        public int Runtime { get; set; }

        public string Status { get; set; }

        public string Tagline { get; set; }

        public string Title { get; set; }

        public string Trailer_path { get; set; }

        public decimal Vote_average { get; set; }

        public int Vote_count { get; set; }

        public string Genres { get; set; }

    }
}
