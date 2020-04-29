using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.BusinessLogic;
using WebApplication1.Domain.Entities;
using WebApplication1.Web.Models;

namespace WebApplication1.Web.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly ISession _session;

        public AuthentificationController()
        {
            var bl = new BusinessLogics();
            _session = bl.GetSessionBL();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Username = login.Username,
                    Password = login.Password,
                    LoginDateTime = DateTime.Now
                };

                var userLogin = _session.UserLogin(data);

                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.Username);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                }
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserRegisterModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password != user.RePassword)
                {
                    ModelState.AddModelError("", "Password are not the same");
                    return View();
                }

                var data = new URegisterData {
                    Login = user.Login,
                    Password = user.Password,
                    RegisterDate = DateTime.Now
                };

                var registerResponse = _session.UserRegister(data);
                if (registerResponse.Status)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", registerResponse.StatusMsg);
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            var apiCookie = Request.Cookies["X-KEY"];
            var sessionExist = _session.UserLogout(apiCookie.Value);
            if (sessionExist.Status)
            {
                return new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
            else
            {
                ModelState.AddModelError("", sessionExist.StatusMsg);
            }
            return View();
        }
    }
}
