using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MovieTheaterApp.API.Data;
using Microsoft.EntityFrameworkCore;
using MovieTheaterApp.API.Services;
using MovieTheaterApp.API.Entities;
using MovieTheaterApp.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieTheaterApp.API
{
    public class Startup
    {
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);            
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyApp",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("SuperUsers", p => p.RequireClaim("SuperUser", "true"));
            });
            services.AddDbContext<MovieTheaterContext>(options => options.UseSqlServer(_config.GetConnectionString("MovieTheaterApp")));
            services.AddScoped<IMovieTheaterRepository, MovieTheaterRepository>();
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MovieTheaterContext>();
            services.AddTransient<IdentitySeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory, 
            MovieTheaterContext movieTheaterContext,
            IdentitySeeder identitySeeder)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            movieTheaterContext.EnsureSeedDataForContext();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MovieDtoAdd, Movie>();
                cfg.CreateMap<MovieDto, Movie>();
                cfg.CreateMap<Movie, MovieDto>();
                cfg.CreateMap<Review, ReviewDto>();
                cfg.CreateMap<ReviewDto, Review>();
                cfg.CreateMap<ReviewDtoAdd, Review>();
                cfg.CreateMap<User, UserDto>();
            });

            app.UseIdentity();

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = _config["Tokens:Issuer"],
                    ValidAudience = _config["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"])),
                    ValidateLifetime = true
                }
            });

            app.UseMvc();

            identitySeeder.Seed().Wait();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
