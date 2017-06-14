using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Models
{
    public class HallDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }
    }
}
