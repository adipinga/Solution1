using System.Web;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Web.Extensions
{
	public static class HttpContextExtensions
    {
		public static UserEntity GetMySessionObject(this HttpContext current)
		{
			return (UserEntity)current?.Session["__SessionObject"];
		}

		public static void SetMySessionObject(this HttpContext current, UserEntity profile)
		{
			current.Session.Add("__SessionObject", profile);
		}
	}
}