using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
