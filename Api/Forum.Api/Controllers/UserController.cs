using Forum.Api.Infrastructure.Configuration;
using Forum.Business.DTO;
using Forum.Business.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RaphaelSantos.Framework.Business;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Api.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly SignInManager<User> SignInManager;
        private readonly UserManager<User> UserManager;
        private readonly IOptions<ApplicationSettings> ApplicationSettings;

        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<ApplicationSettings> applicationSettings
            )
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.ApplicationSettings = applicationSettings;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO model)
        {
            var resultLogin = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (resultLogin.Succeeded)
            {
                var user = UserManager.Users.SingleOrDefault(r => r.UserName == model.UserName);

                var token = GenerateJwtToken(user.Email, user);

                var result = new UserLoggedDTO
                {
                    User = new UserDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        UserName = user.UserName
                    },
                    Token = token
                };

                return Ok(result);
            }
            else
            {
                var rule = new BrokenRule("Invalid username or password.");
                rule.Add("UserName", "Invalid username or password");

                throw new RuleException(rule);
            }

        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Post([FromBody] UserRegisterDTO model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name
            };

            var resultCreate = await UserManager.CreateAsync(user, model.Password);

            if (resultCreate.Succeeded)
            {
                await SignInManager.SignInAsync(user, false);

                var token = GenerateJwtToken(model.Email, user);

                user = await UserManager.FindByEmailAsync(model.Email);

                var result = new UserLoggedDTO
                {
                    User = new UserDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        UserName = user.UserName
                    },
                    Token = token
                };

                return Ok(result);
            }
            else
            {
                if (resultCreate.Errors.Count() > 0)
                {
                    var rule = new BrokenRule("Error!");

                    foreach (var item in resultCreate.Errors)
                    {
                        rule.Add("UserName", item.Description);
                    }

                    throw new RuleException(rule);
                }
                else
                {
                    var rule = new BrokenRule("Error!");
                    rule.Add("UserName", "Register failed");

                    throw new RuleException(rule);
                }
            }
        }

        private object GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApplicationSettings.Value.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(ApplicationSettings.Value.JwtExpireDays));

            var token = new JwtSecurityToken(
                ApplicationSettings.Value.JwtIssuer,
                ApplicationSettings.Value.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
