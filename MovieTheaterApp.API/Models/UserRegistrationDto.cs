using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Models
{
    public class UserRegistrationDto
    {
        [Required, MaxLength(256)]
        public string Username { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
