using AutoMapper;
using System.Web.Mvc;
using WebApplication1.BusinessLogic;
using WebApplication1.Web.App_Start;
using WebApplication1.Web.Extensions;
using WebApplication1.Web.Models;

namespace WebApplication1.Web.Controllers
{
	public class BaseController : Controller
    {
        private readonly ISession _session;
		public readonly Mapper _mapper;

		public BaseController()
        {
			_mapper = new Mapper(AutoMapperConfig.Initialize());
			var bl = new InstanceBL();
            _session = bl.GetSessionBL();
        }

		public void SessionStatus()
		{
			var apiCookie = Request.Cookies["X-KEY"];
			if (apiCookie != null)
			{
				var profile = _session.GetUserByCookie(apiCookie.Value);
				if (profile != null)
				{
					System.Web.HttpContext.Current.SetMySessionObject(profile);
					System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
				}
				else
				{
					System.Web.HttpContext.Current.Session.Clear();
					System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
				}
			}
			else
			{
				System.Web.HttpContext.Current.Session.Clear();
				System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
			}
		}

		public UserData GetUser()
		{
			SessionStatus();
			if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
			{
				var user = System.Web.HttpContext.Current.GetMySessionObject();
				UserData u = new UserData
				{
					Username = user.Username,
					Level = user.Level
				};
				return u;
			}
			return new UserData();
		}
	}
}