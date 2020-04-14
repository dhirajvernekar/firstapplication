using System.Linq;
using AutoMapper;
using DatingApp.api.DTOS;
using DatingApp.api.Models;

namespace DatingApp.api.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User,UserForListsDTO>().
             ForMember(dest=>dest.PhotoUrl,opt=>
             opt.MapFrom(src=> src.Photos.FirstOrDefault(p=>p.IsMain).Url))
              .ForMember(dest=>dest.Age,opt=>
             opt.MapFrom(src=> src.DateOfBirth.CalculateAge()));
            CreateMap<User,UserForDetailedDTO>().
            ForMember(dest=>dest.PhotoUrl,opt=>
             opt.MapFrom(src=> src.Photos.FirstOrDefault(p=>p.IsMain).Url))
             .ForMember(dest=>dest.Age,opt=>
             opt.MapFrom(src=> src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotosForDetailedDTO>();
        }
    }
}