using AutoMapper;
using DatingApp.api.DTOS;
using DatingApp.api.Models;

namespace DatingApp.api.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User,UserForListsDTO>();
            CreateMap<User,UserForDetailedDTO>();
        }
    }
}