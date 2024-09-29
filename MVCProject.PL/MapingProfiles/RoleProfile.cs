using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MVCProject.PL.ViewModels;

namespace MVCProject.PL.MapingProfiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ForMember(D=>D.RoleName,o=>o.MapFrom(s=>s.Name)).ReverseMap();
        }
    }
}
