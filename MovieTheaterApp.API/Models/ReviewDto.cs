using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Models
{
    public class ReviewDto
    {
        public int Id { get; set; }
                
        public double? Score { get; set; }

        public string Text { get; set; }

        public int MovieId { get; set; }

        public UserDto User { get; set; }
    }
}
