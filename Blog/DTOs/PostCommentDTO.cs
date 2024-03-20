using Blog.Models;

namespace Blog.DTOs
{
	public class PostCommentDTO
	{
		public string Title { get; set; }
		public string Description { get; set; }

		public List<Comment> Comments { get; set; }
	}
}
