using AutoMapper;
using MVCProject.DAL.Entities;
using MVCProject.PL.ViewModels;

namespace MVCProject.PL.MapingProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
