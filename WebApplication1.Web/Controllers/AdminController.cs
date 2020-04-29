using System;
using System.Web.Mvc;
using WebApplication1.BusinessLogic;
using WebApplication1.Domain.Entities;
using WebApplication1.Web.Controllers.Attributes;
using WebApplication1.Web.Extensions;
using WebApplication1.Web.Models;

namespace WebApplication1.Web.Controllers
{
    [AdminMod]
    public class AdminController : BaseController
    {
        private readonly IBlog _blog;

        public AdminController()
        {
            var bl = new InstanceBL();
            _blog = bl.GetBlogBL();
        }

        public ActionResult Index()
        {
            SessionStatus();
            return View(_blog.GetAllPosts());
        }

        public ActionResult Add()
        {
            SessionStatus();
            return View();
        }

        [HttpPost]
        public ActionResult Add(PostViewModel post)
        {
            if (post.Title == null || post.PostContent == null)
            {
                return View();
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();

            try
            {
                var p = _mapper.Map<PostEntity>(post);

                if (p.ImageUrl == null)
                {
                    p.ImageUrl = "#";
                }

                p.Date = DateTime.Now;

                _blog.TryAddPost(p);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Edit(int id)
        {
            SessionStatus();
            var post = _blog.GetPostById(id);
            if (post != null)
            {
                return View(post);
            } else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(PostEntity post)
        {
            if (post == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var result = _blog.TryEditPost(post);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Delete(int? id)
        {
            SessionStatus();
            var post = _blog.GetPostById(Convert.ToInt32(id));
            if (post != null)
            {
                return View(post);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _blog.TryDeletePost(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}