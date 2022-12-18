using AccountingInformationSystem.Administration.DataModels;
using AccountingInformationSystem.Data.Entities;
using AutoMapper;

namespace AccountingInformationSystem.Administration.Infrastructure.AutoMappers
{
    public class AdminAutoMapperProfile:Profile
    {
        public AdminAutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDataModel>().ReverseMap();
            CreateMap<User, UserDataModel>().ReverseMap();
        }
    }
}
