using WebApplication1.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using WebApplication1.BusinessLogic.Entities;

namespace WebApplication1.BusinessLogic
{
    public class BlogApi
    {
        private readonly IMapper _mapper;
        public BlogApi()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PostEntity, Post>().ReverseMap();
                cfg.CreateMap<UserEntity, User>().ReverseMap();
            });

            _mapper = new Mapper(config);
        }

        internal PostEntity PostById(int id)
        {

            Post currentPost;
            using (var db = new SiteContext())
            {
                currentPost = db.Posts.Where(x => x.PostId == id).SingleOrDefault();
                if (currentPost == null)
                    return null;
            }

            return _mapper.Map<PostEntity>(currentPost);
        }

        internal List<PostEntity> LastPosts()
        {
            List<Post> blogPosts;
            using (var db = new SiteContext())
            {
                int toTake = db.Posts.Count() > 3 ? 3 : db.Posts.Count();
                blogPosts = db.Posts
                    .OrderByDescending(p => p.PostId)
                    .Take(toTake)
                    .ToList();
            }

            return _mapper.Map<List<PostEntity>>(blogPosts);
        }

        internal List<PostEntity> AllPosts()
        {
            List<Post> blogPosts;
            using (var db = new SiteContext())
            {
                blogPosts = db.Posts
                    .OrderByDescending(p => p.Date)
                    .ToList();
            }

            return _mapper.Map<List<PostEntity>>(blogPosts);
        }

        internal bool AddPost(PostEntity post)
        {
            if (post != null)
            {
                var p = _mapper.Map<Post>(post);

                using (var db = new SiteContext())
                {
                    db.Posts.Add(p);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        internal bool EditPost(PostEntity post)
        {
            var result = PostExistById(post.PostId);
            if (result == true)
            {
                using (var db = new SiteContext())
                {
                    var currentPost = db.Posts.SingleOrDefault(p => p.PostId == post.PostId);
                    if (currentPost == null)
                    {
                        return false;
                    }
                    currentPost.Title = post.Title;
                    currentPost.ImageUrl = post.ImageUrl;
                    currentPost.PostContent = post.PostContent;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        internal bool DeletePost(int postId)
        {
            if (postId < 0) return false;

            using (var db = new SiteContext())
            {
                var post = db.Posts.First(p => p.PostId == postId);
                if (post == null)
                {
                    return false;
                }
                db.Posts.Remove(post);
                db.SaveChanges();
                return true;
            }
        }

        private User GetUserByPosterId(int id)
        {
            using (var db = new SiteContext())
            {
                return db.Users.SingleOrDefault(u => u.Id == id);
            }
        }

        private bool PostExistById(int id)
        {
            using (var db = new SiteContext())
            {
                return db.Posts.Any(p => p.PostId == id);
            }
        }
    }
}