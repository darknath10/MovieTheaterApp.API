using MovieTheaterApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Models
{
    public class MovieWithShowsDto : MovieDto
    {
        public ICollection<ShowDto> Shows { get; set; }
    }
}
