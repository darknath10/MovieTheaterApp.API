using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Models
{
    public class ShowDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public HallDto Hall { get; set; }

        
    }
}
