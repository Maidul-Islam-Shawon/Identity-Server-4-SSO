using API.Models;
using AutoMapper;
using DataAccess.Entities;

namespace API.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CoffeeShopModels, CoffeeShop>().ReverseMap();
        }
    }
}
