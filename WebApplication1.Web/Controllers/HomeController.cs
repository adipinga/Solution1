using System.Linq;
using System.Web.Mvc;
using WebApplication1.BusinessLogic;

namespace WebApplication1.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlog _blog;

        public HomeController()
        {
            var bl = new InstanceBL();
            _blog = bl.GetBlogBL();
        }

        public ActionResult Index()
        {
            SessionStatus();
            return View(_blog.GetAllPosts());
        }

        public ActionResult Detail(int id)
        {
            SessionStatus();
            return View(_blog.GetPostById(id));
        }
        public ActionResult Top()
        {
            SessionStatus();
            return View();
        }

        public ActionResult About()
        {
            SessionStatus();
            return View();
        }
    }
}