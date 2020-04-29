using System.Data.Entity;
using WebApplication1.BusinessLogic.Entities;

namespace WebApplication1.BusinessLogic
{
    class SiteContext : DbContext
    {
        public SiteContext() : base("name=WebApplication1")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
    }
}
