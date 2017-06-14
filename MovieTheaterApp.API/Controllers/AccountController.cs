using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieTheaterApp.API.Entities;
using MovieTheaterApp.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheaterApp.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowMyApp")]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private IPasswordHasher<User> _pwdHasher;
        private IConfigurationRoot _config;

        public AccountController(UserManager<User> userManager, IPasswordHasher<User> pwdHasher, IConfigurationRoot config)
        {
            _userManager = userManager;
            _pwdHasher = pwdHasher;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = new User { UserName = model.Username, Email = model.EmailAdress };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) return Ok();
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    if (_pwdHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _config["Tokens:Issuer"],
                            audience: _config["Tokens:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(20),
                            signingCredentials: credentials);

                        return Ok(new
                        {
                            username = user.UserName,
                            emailAddress = user.Email,
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expires = token.ValidTo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return BadRequest("An error occured");
        }
    }
}
