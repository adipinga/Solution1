using WebApplication1.Domain.Entities;
using System.Collections.Generic;


namespace WebApplication1.BusinessLogic
{
    public class BlogBL : BlogApi, IBlog
    {
        public PostEntity GetPostById(int id) => PostById(id);
        
        public List<PostEntity> GetLastPosts() => LastPosts();

        public List<PostEntity> GetAllPosts() => AllPosts();

        public bool TryEditPost(PostEntity post) => EditPost(post);

        public bool TryDeletePost(int id) => DeletePost(id);

        public bool TryAddPost(PostEntity post) => AddPost(post);

    }
}