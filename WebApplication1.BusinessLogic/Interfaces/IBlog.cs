using WebApplication1.Domain.Entities;
using System.Collections.Generic;


namespace WebApplication1.BusinessLogic
{
    public interface IBlog
    {
        PostEntity GetPostById(int id);
        List<PostEntity> GetLastPosts();
        List<PostEntity> GetAllPosts();
        bool TryAddPost(PostEntity post);
        bool TryEditPost(PostEntity post);
        bool TryDeletePost(int id);
    }
}