using AutoMapper;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebApplication1.BusinessLogic.Entities;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.Enums;
using WebApplication1.Helpers;

namespace WebApplication1.BusinessLogic
{
    public class UserApi
    {
		private readonly IMapper _mapper;
		public UserApi()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<User, UserEntity>().ReverseMap();
			});
			_mapper = new Mapper(config);
		}

		internal UserEntity UserCookie(string apiCookieValue)
        {
			User curentUser;

			using (var db = new SiteContext())
			{
				var session = db.Sessions.FirstOrDefault(s => s.CookieString == apiCookieValue && s.ExpireTime > DateTime.Now);
				if (session == null) return null;

				curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
				if (curentUser == null) return null;
			}

			return _mapper.Map<UserEntity>(curentUser);
		}

		// Generate the cookie and put in database
		internal HttpCookie Cookie(string username)
		{
			var apiCookie = new HttpCookie("X-KEY")
			{
				Value = CookieGenerator.Create(username)
			};

			using (var db = new SiteContext())
			{
				Session curent;

				curent = db.Sessions.FirstOrDefault(u => u.Username == username);

				if (curent != null)
				{
					curent.CookieString = apiCookie.Value;
					curent.ExpireTime = DateTime.Now.AddDays(1);

					db.Entry(curent).State = EntityState.Modified;
					db.SaveChanges();
				}
				else
				{
					db.Sessions.Add(new Session
					{
						Username = username,
						CookieString = apiCookie.Value,
						ExpireTime = DateTime.Now.AddDays(1)
					});
					db.SaveChanges();
				}
			}

			return apiCookie;
		}

		internal UActionResp UserLoginAction(ULoginData data)
        {
            User result;
            using (var db = new SiteContext())
            {
                result = db.Users.FirstOrDefault(u => u.Username == data.Username && u.Password == data.Password);
            }
            if (result == null)
            {
                return new UActionResp { Status = false, StatusMsg = "The username or password is incorrect" };
            }
            return new UActionResp { Status = true };
        }

		internal UActionResp UserLogoutAction(string cookie)
		{
			Session result;

			using (var db = new SiteContext())
			{
				result = db.Sessions.FirstOrDefault(s => s.CookieString == cookie);
				if (result != null)
				{
					result.ExpireTime = DateTime.Now.AddHours(-1);
					db.SaveChanges();
					return new UActionResp { Status = true };
				}
			}
			return new UActionResp { Status = false, StatusMsg = "User already signed out" };
		}

		internal UActionResp UserRegisterAction(URegisterData data)
		{
			User result;

			using (var db = new SiteContext())
			{
				result = db.Users.FirstOrDefault(u => u.Username == data.Login);
				if (result == null)
				{
					var user = new User
					{
						Username = data.Login,
						Email = data.Login,
						Password = data.Password,
						Level = URole.User,
						RegisterDate = data.RegisterDate
					};

					try
					{
						db.Users.Add(user);
						db.SaveChanges();
					}
					catch (DbEntityValidationException ex)
					{
						// Retrieve the error messages as a list of strings.
						var errorMessages = ex.EntityValidationErrors
								.SelectMany(x => x.ValidationErrors)
								.Select(x => x.ErrorMessage);

						// Join the list to a single string.
						var fullErrorMessage = string.Join(Environment.NewLine, errorMessages);
						return new UActionResp { Status = false, StatusMsg = fullErrorMessage };
					}
					return new UActionResp { Status = true };
				}
			}
			return new UActionResp { Status = false, StatusMsg = "This user already exists" };
		}
	}
}
  