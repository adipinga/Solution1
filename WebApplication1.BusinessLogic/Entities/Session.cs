using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.BusinessLogic.Entities
{
	public class Session
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SessionId { get; set; }

		[Required]
		[StringLength(30)]
		public string Username { get; set; }

		[Required]
		public string CookieString { get; set; }

		[Required]
		public DateTime ExpireTime { get; set; }
	}
}