using Blog.Models;

namespace Blog.DTOs
{
	public class CommentDTO
	{
		public string CommentText { get; set; }

		public int PostId { get; set; }

		public int UserId { get; set; }
	}
}
