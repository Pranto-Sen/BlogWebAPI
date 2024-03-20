using AutoMapper;
using Blog.Data;
using Blog.DTOs;
using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		private readonly BlogDbContext dbContext;
		private readonly IMapper mapper;

		public CommentsController(BlogDbContext dbContext , IMapper mapper)
        {
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

        [HttpPost]
		[Route("CreateComment")]
		public IActionResult Create([FromBody] CommentDTO CommentDTO)
		{
			var CommentModel = mapper.Map<Comment>(CommentDTO);

			dbContext.Comments.Add(CommentModel);
			dbContext.SaveChanges();

			return Ok(CommentModel);
		}

		[HttpGet]
		[Route("GetAll")]
		public IActionResult GetAll()
		{
			var Comments = dbContext.Comments
				.ToList();

			var CommentsDTO = mapper.Map<List<CommentDTO>>(Comments);

			return Ok(CommentsDTO);
		}

		[HttpPut]
		[Route("update/{id}")]
		public IActionResult Update([FromBody] CommentDTO CommentDTO, [FromRoute] int id)
		{
			var comment = dbContext.Comments.FirstOrDefault(c=> c.Id == id);
			if (comment == null)
			{
				return BadRequest("not Found");
			}
			comment.CommentText = CommentDTO.CommentText;
			comment.UserId = CommentDTO.UserId;
			comment.PostId = CommentDTO.PostId;

			dbContext.SaveChanges();
			return Ok(comment);
		}

		[HttpDelete]
		[Route("Delete/{id}")]
		public IActionResult Delete([FromRoute] int id)
		{
			var comment = dbContext.Comments.FirstOrDefault(c => c.Id == id);
			if (comment == null)
			{
				return BadRequest("not Found");
			}
			dbContext.Comments.Remove(comment);
			dbContext.SaveChanges();
			return Ok();
		}
	}
}
