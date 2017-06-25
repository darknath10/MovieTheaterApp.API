using Microsoft.AspNetCore.Identity;
using MovieTheaterApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Data
{
    public class IdentitySeeder
    {
        private UserManager<User> _userManager;

        public IdentitySeeder(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Seed()
        {
            var user = await _userManager.FindByNameAsync("admin");

            if(user == null)
            {
                user = new User()
                {
                    UserName = "admin",
                    Email = "admin@somemail.com"
                };

                var userResult = await _userManager.CreateAsync(user, "P@ssw0rd");
                var claimResult = await _userManager.AddClaimAsync(user, new Claim("SuperUser", "true"));

                if (!userResult.Succeeded || !claimResult.Succeeded) throw new InvalidOperationException("Failed to build user.");
            }
        }
    }
}
