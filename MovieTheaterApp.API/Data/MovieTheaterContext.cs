using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTheaterApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Data
{
    public class MovieTheaterContext : IdentityDbContext<User>
    {
        public MovieTheaterContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Show> Shows { get; set; }
    }
}
