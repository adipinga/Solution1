using AutoMapper;
using WebApplication1.BusinessLogic.Entities;
using WebApplication1.Domain.Entities;
using WebApplication1.Web.Models;

namespace WebApplication1.Web.App_Start
{
    public class AutoMapperConfig
    {
		public static MapperConfiguration Initialize()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<UserLogin, ULoginData>();
				cfg.CreateMap<User, UserEntity>();
				cfg.CreateMap<UserEntity, UserData>();
				cfg.CreateMap<UserRegisterModel, URegisterData>();
				cfg.CreateMap<URegisterData, User>();
				cfg.CreateMap<Post, PostEntity>();
				cfg.CreateMap<PostEntity, Post>();
				cfg.CreateMap<PostViewModel, PostEntity>();
				cfg.CreateMap<UserModel, UserEntity>().ReverseMap();
			});
			return config;
		}
	}
}