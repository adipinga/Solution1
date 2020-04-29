using System.Web;
using WebApplication1.Domain.Entities;

namespace WebApplication1.BusinessLogic
{
    public interface ISession
    {
        UActionResp UserRegister(URegisterData data);
        UActionResp UserLogin(ULoginData data);
        UActionResp UserLogout(string cookie);
        HttpCookie GenCookie(string username);
        UserEntity GetUserByCookie(string apiCookieValue);
    }
}