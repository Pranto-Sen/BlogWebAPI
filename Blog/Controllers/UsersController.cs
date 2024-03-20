using AutoMapper;
using Blog.Data;
using Blog.DTOs;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly BlogDbContext DbContext;
		private readonly IMapper Mapper;

		public UsersController(BlogDbContext DbContext, IMapper Mapper)
		{
			this.DbContext = DbContext;
			this.Mapper = Mapper;
		}

		[HttpGet]
		[Route("GetAllUser")]
		public IActionResult GetAll()
		{
			var users = DbContext.Users.ToList();

			var usersDTO = Mapper.Map<List<UserDTO>>(users);

			return Ok(usersDTO);
		}

		[HttpGet]
		[Route("GetAllUserWithPost")]
		public IActionResult GetAllUserWithPost()
		{
			//var users = DbContext.Users.ToList();
			var users = DbContext.Users
					.Include(c => c.Posts)
					.ToList();

			var usersDTO = Mapper.Map<List<UserPostDTO>>(users);

			return Ok(usersDTO);
		}

		[HttpGet]
		[Route("GetUserWithPostComment/{id}")]
		public IActionResult GetUserWithPostComment([FromRoute] int id )
		{
			//var users = DbContext.Users.ToList();

			var user = DbContext.Users
				    .Include(c => c.Posts)
					.ThenInclude(c => c.Comments)
					.FirstOrDefault(x => x.Id == id);

			if (user == null)
			{
				return NotFound("User not Found");
			}
			

			var usersDTO = Mapper.Map<UserPostDTO>(user);

			return Ok(usersDTO);
		}

		[HttpGet]
		[Authorize]
		[Route("GetAllUserWithPostComment")]
		public IActionResult GetAllUserWithPostComment()
		{
			//var users = DbContext.Users.ToList();
			var users = DbContext.Users
				    .Include(c=> c.Posts)
					.ThenInclude(c=> c.Comments)
					.ToList();

			var usersDTO = Mapper.Map<List<UserPostDTO>>(users);

			return Ok(usersDTO);
		}

		[HttpGet]
		[Route("{id}")]
		public IActionResult GetById([FromRoute] int id)
		{
			var user = DbContext.Users.FirstOrDefault(x => x.Id == id);

			if (user == null)
			{
				return BadRequest("Not Found");
			}

			var userDTO = Mapper.Map<UserDTO>(user);

			return Ok(userDTO);
		}

		[HttpPost]
		[Route("Register")]
		public IActionResult Create([FromBody] UserDTO UserDTO)
		{
			var userModel = Mapper.Map<User>(UserDTO);

			DbContext.Users.Add(userModel);
			DbContext.SaveChanges();

			return Ok(userModel);
		}

		[HttpPut]
		[Route("update/{id}")]
		public IActionResult Update([FromRoute] int id ,[FromBody] UserDTO UserDTO)
		{
			var user = DbContext.Users.FirstOrDefault(x=> x.Id == id);

			if(user == null)
			{
				return BadRequest();
			}

			user.Name = UserDTO.Name;
			user.Email = UserDTO.Email;
			user.Phone = UserDTO.Phone;
			user.UserName = UserDTO.UserName;
			user.Password = UserDTO.Password;

			DbContext.SaveChanges();

			return Ok(user);
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public IActionResult Delete([FromRoute] int id)
		{
			var user = DbContext.Users.FirstOrDefault(x=> x.Id == id);

			if(user == null)
			{
				return BadRequest();
			}

			DbContext.Users.Remove(user);
			DbContext.SaveChanges();
			return Ok();
		}



	}
}
