using System.Web;
using WebApplication1.Domain.Entities;

namespace WebApplication1.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public UserEntity GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

        public HttpCookie GenCookie(string username)
        {
            return Cookie(username);
        }

        public UActionResp UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }

        public UActionResp UserRegister(URegisterData data)
        {
            return UserRegisterAction(data);
        }

        public UActionResp UserLogout(string cookie)
        {
            return UserLogoutAction(cookie);
        }
    }
}
