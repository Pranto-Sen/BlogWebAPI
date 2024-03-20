using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
	public class Comment
	{
		[Key] 
		public int Id { get; set; }
		[Required]
		public string CommentText { get; set; }

		
		public int PostId { get; set; }
		//public Post Post { get; set; }

		public int UserId { get; set; }
		//public User User { get; set; }
	

	}
}
