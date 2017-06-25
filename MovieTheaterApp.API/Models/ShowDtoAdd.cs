using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Models
{
    public class ShowDtoAdd
    {
        public int MovieId { get; set; }

        public int HallId { get; set; }

        public DateTime Date { get; set; }
    }
}
