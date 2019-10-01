using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Elibrary.Models;
using Microsoft.IdentityModel.Tokens;

namespace Elibrary.Controllers
{

    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
           _signInManager = signInManager;
            _configuration = configuration;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] Register model)
        {
               if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var validUser = await _userManager.FindByNameAsync(model.Email);
            if(validUser == null) {
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Type ?? "User");
             }
                return Ok(new {
                 message = "user created successfully",
                 Username = user.UserName });
            }
                return Conflict(new {
                   error = "user already exist Log in"
                });
            }


        [Route("login")] // /login
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] Login model)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                 var user = await _userManager.FindByNameAsync(model.Username);
                 var Roles = await _userManager.GetRolesAsync(user);
                var claim = new List <Claim> {
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(ClaimTypes.NameIdentifier, user.Id)
 
                };
                claim.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var signinKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Site"],
                  audience: _configuration["Jwt:Site"],
                  claims: claim,
                  expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(
                  new
                  {
                      token = new JwtSecurityTokenHandler().WriteToken(token),
                      expiration = token.ValidTo
                  });
            }
            return Unauthorized();
        }
    }
}
