using AutoMapper;
using MVCProject.DAL.Entities;
using MVCProject.PL.ViewModels;

namespace MVCProject.PL.MapingProfiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
