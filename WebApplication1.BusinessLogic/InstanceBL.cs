using System;

namespace WebApplication1.BusinessLogic
{
    public class InstanceBL
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IBlog GetBlogBL()
        {
            return new BlogBL();
        }
    }
}