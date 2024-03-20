using Blog.Models;

namespace Blog.DTOs
{
	public class UserPostDTO
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }

		public List<Post> Posts { get; set; }

		//public List<Comment> Comments { get; set; }
	}
}
