using System;
using WebApplication1.Domain.Enums;

namespace WebApplication1.Web.Models
{
	public class UserModel
    {
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public URole Level { get; set; }
		public string AvatarUrl { get; set; }
		public DateTime RegisterDate { get; set; }
	}
}