using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Entities
{
    public class Show
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("HallId")]
        public Hall Hall { get; set; }

        public int HallId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        public int MovieId { get; set; }
    }
}
