using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Models
{
    public class ReviewDtoAdd
    {
        [Range(0.5d, 10.0d)]
        public double? Score { get; set; }

        public string Text { get; set; }

        public int MovieId { get; set; }

        public string UserName { get; set; }
    }
}
