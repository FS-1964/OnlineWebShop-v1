using AutoMapper;

using System.Security.Claims;
using WebShop.DAL.Datacontext;
using WebShop.DAL.Entities;
using WebShop.PL.ViewModels;

namespace WebShop.PL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<ExternalLoginModel, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
                .ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.Principal.FindFirst(ClaimTypes.GivenName).Value))
                .ForMember(u => u.LastName, opt => opt.MapFrom(x => x.Principal.FindFirst(ClaimTypes.Surname).Value));
        }
    }
}
