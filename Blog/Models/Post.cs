using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
	public class Post
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        public  DateTime PublishTime { get; set; } = DateTime.Now;
		
		public int UserId { get; set; }

		//public User User { get; set; }

		public List<Comment> Comments { get; set; }

	
	}
}
