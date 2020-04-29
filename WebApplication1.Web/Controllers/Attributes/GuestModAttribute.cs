using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1.Web.Controllers.Attributes
{
	public class GuestModAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if ((string)HttpContext.Current.Session["LoginStatus"] == "login")
			{
				filterContext.Result = new RedirectToRouteResult(new
					RouteValueDictionary(new { controller = "Home", action = "Index" }));
			}
		}
	}
}