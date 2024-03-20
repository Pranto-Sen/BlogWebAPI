using Blog.Models;

namespace Blog.DTOs
{
	public class UserDTO
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }

		public List<Post> Posts { get; set; }
		public List<Comment> Comments { get; set; }



	}
}
