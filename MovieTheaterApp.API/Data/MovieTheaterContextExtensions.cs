using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MovieTheaterApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Data
{
    public static class MovieTheaterContextExtensions
    {        
        public static void EnsureSeedDataForContext(this MovieTheaterContext context)
        {
            if (!context.Halls.Any())
            {
                var halls = new List<Hall>()
                {
                    new Hall() { Name = "A", Capacity = 150 },
                    new Hall() { Name = "B", Capacity = 100 },
                    new Hall() { Name = "C", Capacity = 80 },
                    new Hall() { Name = "D", Capacity = 50 },
                    new Hall() { Name = "E", Capacity = 30 }
                };

                context.AddRange(halls);
                context.SaveChanges();
            }

            if (!context.Movies.Any())
            {
                // seed data
                var movies = new List<Movie>()
                {
                    new Movie()
                    {
                        Backdrop_path = "http://image.tmdb.org/t/p/original/xu9zaAevzQ5nnrsXN6JcahLnG4i.jpg",
                        Genres = "Adventure, Drama, Science Fiction",
                        Overview = "Interstellar chronicles the adventures of a group of explorers who make use of a newly discovered wormhole to surpass the limitations on human space travel and conquer the vast distances involved in an interstellar voyage.",
                        Popularity = 33.97304m,
                        Poster_path = "http://image.tmdb.org/t/p/original/nBNZadXqJSdt05SHLqgT0HuC5Gm.jpg",
                        Release_date = "2014-11-05",
                        Runtime = 169,
                        Status = "Released",
                        Tagline = "Mankind was born on Earth. It was never meant to die here.",
                        Title = "Interstellar",
                        Trailer_path = "https://www.youtube.com/embed/2LqzF5WauAw",
                        Tmdb_id = 157336,
                        Vote_average = 8,
                        Vote_count = 8564,
                        Shows = new List<Show>()
                        {
                            new Show() { Date = new DateTime(2017, 7, 10, 21, 0, 0), HallId = 1}
                        }
                    },
                    new Movie()
                    {
                        Backdrop_path = "http://image.tmdb.org/t/p/original/vC9H1ZVdXi1KjH4aPfGB54mvDNh.jpg",
                        Genres = "Drama, History",
                        Overview = "On 15 January 2009, the world witnessed the 'Miracle on the Hudson' when Captain 'Sully' Sullenberger glided his disabled plane onto the frigid waters of the Hudson River, saving the lives of all 155 aboard. However, even as Sully was being heralded by the public and the media for his unprecedented feat of aviation skill, an investigation was unfolding that threatened to destroy his reputation and career.",
                        Popularity = 5.372689m,
                        Poster_path = "http://image.tmdb.org/t/p/original/h6O5OE3ueRVdCc7V7cwTiQocI7D.jpg",
                        Release_date = "2016-09-07",
                        Runtime = 96,
                        Status = "Released",
                        Tagline = "The Untold Story Behind the Miracle on the Hudson.",
                        Title = "Sully",
                        Trailer_path = "https://www.youtube.com/embed/mjKEXxO2KNE",
                        Tmdb_id = 363676,
                        Vote_average = 6.9m,
                        Vote_count = 1512,
                        Shows = new List<Show>()
                        {
                            new Show() { Date = new DateTime(2017, 7, 10, 21, 0, 0), HallId = 2}
                        }
                    },
                    new Movie()
                    {
                        Backdrop_path = "http://image.tmdb.org/t/p/original/f6Gr8UD7nepAAkJE7nYP85J3EbI.jpg",
                        Genres = "Action, Adventure, Science Fiction",
                        Overview = "Following the events of Captain America: Civil War, Peter Parker, with the help of his mentor Tony Stark, tries to balance his life as an ordinary high school student in Queens, New York City with fighting crime as his superhero alter ego Spider-Man as a new threat, the Vulture, emerges.",
                        Popularity = 2.818291m,
                        Poster_path = "http://image.tmdb.org/t/p/original/p1rcJ0ipSHS7UVu9wPOwUpFnRRE.jpg",
                        Release_date = "2017-07-06",
                        Runtime = 124,
                        Status = "Post Production",
                        Tagline = "Homework can wait. The city can't.",
                        Title = "Spider-Man: Homecoming",
                        Trailer_path = "https://www.youtube.com/embed/rk-dF1lIbIg",
                        Tmdb_id = 315635,
                        Vote_average = 0,
                        Vote_count = 0,
                        Shows = new List<Show>()
                        {
                            new Show() { Date = new DateTime(2017, 7, 10, 21, 0, 0), HallId = 3}
                        }
                    }
                };

                context.AddRange(movies);
                context.SaveChanges();
            }

            return;
        }
    }
}
