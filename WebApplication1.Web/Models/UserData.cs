using WebApplication1.Domain.Enums;

namespace WebApplication1.Web.Models
{
    public class UserData
    {
        public string Username { get; set; }
        public URole Level { get; set; }
    }
}