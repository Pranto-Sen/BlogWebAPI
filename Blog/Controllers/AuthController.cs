using Blog.Data;
using Blog.DTOs;
using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly BlogDbContext dbContext;
		private readonly IConfiguration configuration;

		public AuthController(BlogDbContext dbContext, IConfiguration configuration)
        {
			this.dbContext = dbContext;
			this.configuration = configuration;
		}


		[Route("/login")]
		[HttpPost]
		public IActionResult Login([FromBody] LoginDTO userDTO)
		{

			var user = dbContext.Users.FirstOrDefault(u => u.UserName == userDTO.UserName && u.Password == userDTO.Password);
			if (user == null)
			{
				return BadRequest("Usernamr and Password not mached");
			}

			string token = CreateToken(user);

			return Ok(token);
		}


		private string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
			};


			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				configuration.GetSection("AppSettings:Token").Value!));
			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddSeconds(30),
				signingCredentials: cred
				);
			var jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}
	}
}
