namespace WebApplication1.BusinessLogic
{
    public class BusinessLogics
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}