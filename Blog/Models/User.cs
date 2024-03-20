using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
	public class User
	{
		[Key]
        public int Id { get; set; }
		public string Name { get; set; }
        public string Email { get; set; }
		public string Phone { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }

		public List<Post> Posts { get; set; }

		public List<Comment> Comments { get; set; }
    }
}
