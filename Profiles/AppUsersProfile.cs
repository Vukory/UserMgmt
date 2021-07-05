using AutoMapper;
using UserMgmt.Dtos;
using UserMgmt.Models;

namespace UserMgmt.Profiles
{
    public class AppUsersProfile : Profile
    {
        public AppUsersProfile()
        {
            CreateMap<AppUser, AppUserReadDto>();

            CreateMap<AppUserCreateDto, AppUser>();

            CreateMap<AppUser, AppUserUpdateDto>();

            CreateMap<AppUserUpdateDto, AppUser>();

        }
    }
}