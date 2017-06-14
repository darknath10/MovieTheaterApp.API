using MovieTheaterApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Data
{
    public static class MovieTheaterContextExtensions
    {
        public static void EnsureSeedDataForContext(this MovieTheaterContext context)
        {
            if (context.Movies.Any())
            {
                return;
            }

            // seed data
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Backdrop_path = "/xu9zaAevzQ5nnrsXN6JcahLnG4i.jpg",
                    Genres = "Adventure, Drama, Science Fiction",
                    Overview = "Interstellar chronicles the adventures of a group of explorers who make use of a newly discovered wormhole to surpass the limitations on human space travel and conquer the vast distances involved in an interstellar voyage.",
                    Popularity = 33.97304m,
                    Poster_path = "/nBNZadXqJSdt05SHLqgT0HuC5Gm.jpg",
                    Release_date = "2014-11-05",
                    Runtime = 169,
                    Status = "Released",
                    Tagline = "Mankind was born on Earth. It was never meant to die here.",
                    Title = "Interstellar",
                    Trailer_path = "2LqzF5WauAw",
                    Tmdb_id = 157336,
                    Vote_average = 8,
                    Vote_count = 8564
                },
                new Movie()
                {
                    Backdrop_path = "/vC9H1ZVdXi1KjH4aPfGB54mvDNh.jpg",
                    Genres = "Drama, History",
                    Overview = "On 15 January 2009, the world witnessed the 'Miracle on the Hudson' when Captain 'Sully' Sullenberger glided his disabled plane onto the frigid waters of the Hudson River, saving the lives of all 155 aboard. However, even as Sully was being heralded by the public and the media for his unprecedented feat of aviation skill, an investigation was unfolding that threatened to destroy his reputation and career.",
                    Popularity = 5.372689m,
                    Poster_path = "/h6O5OE3ueRVdCc7V7cwTiQocI7D.jpg",
                    Release_date = "2016-09-07",
                    Runtime = 96,
                    Status = "Released",
                    Tagline = "The Untold Story Behind the Miracle on the Hudson.",
                    Title = "Sully",
                    Trailer_path = "mjKEXxO2KNE",
                    Tmdb_id = 363676,
                    Vote_average = 6.9m,
                    Vote_count = 1512                    
                },
                new Movie()
                {
                    Backdrop_path = "/f6Gr8UD7nepAAkJE7nYP85J3EbI.jpg",
                    Genres = "Action, Adventure, Science Fiction",
                    Overview = "Following the events of Captain America: Civil War, Peter Parker, with the help of his mentor Tony Stark, tries to balance his life as an ordinary high school student in Queens, New York City with fighting crime as his superhero alter ego Spider-Man as a new threat, the Vulture, emerges.",
                    Popularity = 2.818291m,
                    Poster_path = "/p1rcJ0ipSHS7UVu9wPOwUpFnRRE.jpg",
                    Release_date = "2017-07-06",
                    Runtime = 124,
                    Status = "Post Production",
                    Tagline = "Homework can wait. The city can't.",
                    Title = "Spider-Man: Homecoming",
                    Trailer_path = "rk-dF1lIbIg",
                    Tmdb_id = 315635,
                    Vote_average = 0,
                    Vote_count = 0                    
                }
            };

            context.AddRange(movies);
            context.SaveChanges();
        }
    }
}
