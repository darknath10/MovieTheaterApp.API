using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MovieTheaterApp.API.Data;

namespace MovieTheaterApp.API.Migrations
{
    [DbContext(typeof(MovieTheaterContext))]
    [Migration("20170516202630_MovieTheaterDbInitialMigration")]
    partial class MovieTheaterDbInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieTheaterApp.API.Entities.Hall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("MovieTheaterApp.API.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Backdrop_path");

                    b.Property<string>("Genres");

                    b.Property<string>("Overview");

                    b.Property<decimal?>("Popularity");

                    b.Property<string>("Poster_path");

                    b.Property<string>("Release_date");

                    b.Property<int?>("Runtime");

                    b.Property<string>("Status");

                    b.Property<string>("Tagline");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int?>("Tmdb_id");

                    b.Property<string>("Trailer_path");

                    b.Property<decimal?>("Vote_average");

                    b.Property<int?>("Vote_count");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieTheaterApp.API.Entities.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("HallId");

                    b.Property<int>("MovieId");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.HasIndex("MovieId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("MovieTheaterApp.API.Entities.Show", b =>
                {
                    b.HasOne("MovieTheaterApp.API.Entities.Hall", "Hall")
                        .WithMany("Shows")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MovieTheaterApp.API.Entities.Movie", "Movie")
                        .WithMany("Shows")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
