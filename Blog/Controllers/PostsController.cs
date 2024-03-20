using AutoMapper;
using Blog.Data;
using Blog.DTOs;
using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Blog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly BlogDbContext DbContext;
		private readonly IMapper Mapper;

		public PostsController(BlogDbContext DbContext, IMapper Mapper)
        {
            this.DbContext = DbContext;
			this.Mapper = Mapper;
        }

        [HttpPost]
		[Route("CreatePost")]
		public IActionResult Create([FromBody] PostDTO PostDTO)
		{
			var PostModel = Mapper.Map<Post>(PostDTO);

			DbContext.Posts.Add(PostModel);
			DbContext.SaveChanges();

			return Ok(PostModel);
		}

		[HttpGet]
		[Route("GetAllPost")]
		public IActionResult GetGetAllPostAll()
		{
			var Posts = DbContext.Posts.ToList();

			var PostsDTO = Mapper.Map<List<PostDTO>>(Posts);

			return Ok(PostsDTO);
		}

		[HttpGet]
		[Route("GetAllPostWithComment")]
		public IActionResult GetPostWithComment()
		{
			var Posts = DbContext.Posts
				.Include("Comments")
				.ToList();

			var PostsDTO = Mapper.Map<List<PostCommentDTO>>(Posts);

			return Ok(PostsDTO);
		}


		[HttpGet]
		[Route("GetSinglePost/{id}")]
		public IActionResult GetSinglePost(int id)
		{
			var post = DbContext.Posts.FirstOrDefault(x => x.Id == id);

			if (post == null)
			{
				return NotFound("Post not Found");
			}
			var postDTO = Mapper.Map<PostDTO>(post);

			return Ok(postDTO);
		}


		[HttpGet]
		[Route("GetSinglePostWithComment/{id}")]
		public IActionResult GetSinglePostWithComment([FromRoute] int id)
		{

			var Posts = DbContext.Posts
				.Include("Comments")
				.FirstOrDefault(x=> x.Id == id);
			if(Posts == null)
			{
				return BadRequest("Post not Found");
			}
			var PostsDTO = Mapper.Map<PostCommentDTO>(Posts);

			return Ok(PostsDTO);
		}

		[HttpPut]
		[Route("Update/{id}")]
		public IActionResult Update([FromRoute] int id, [FromBody] PostDTO postDTO)
		{
			var post = DbContext.Posts.FirstOrDefault(x=> x.Id==id);

			if (post == null)
			{
				return NotFound();
			}

			post.Title = postDTO.Title;
			post.Description = postDTO.Description;
			post.UserId = postDTO.UserId;

			DbContext.SaveChanges();
			return Ok(post);
		}

		[HttpDelete]
		[Route("Delete/{id}")]

		public IActionResult Delete([FromRoute] int id)
		{
			var post = DbContext.Posts.FirstOrDefault(x=> x.Id==id);
			if(post == null)
			{
				return NotFound();
			}
			DbContext.Posts.Remove(post);
			DbContext.SaveChanges();
			return Ok(post);
		}

	
	}
}
