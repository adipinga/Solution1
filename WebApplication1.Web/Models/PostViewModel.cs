using System;

namespace WebApplication1.Web.Models
{
	public class PostViewModel
	{
		public int PostId { get; set; }
		public UserModel User { get; set; }
		public string Title { get; set; }
		public string PostContent { get; set; }
		public string ImageUrl { get; set; }
		public DateTime Date { get; set; }
	}
}