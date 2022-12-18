using AccountingInformationSystem.Administration.DataModels;
using AccountingInformationSystem.CreateCommands;
using AccountingInformationSystem.ViewModels;
using AutoMapper;

namespace AccountingInformationSystem.Infrastructure.AutoMappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeDataModel, EmployeesViewModel>()
                .ReverseMap();
            CreateMap<EmployeeDataModel, AddEmployeeCreateCommand>()
                .ReverseMap();
            CreateMap<EmployeeDataModel, UpdateEmployeeCreateCommand>()
                .ForMember(cmd => cmd.TransferDate, e => e.Ignore())
                .ReverseMap();

            CreateMap<UserDataModel, RegistrationCreateCommand>()
                .ReverseMap();
            CreateMap<UserDataModel, UpdateUserCreateCommand>()
                .ForMember(cmd => cmd.TransferDate, e => e.Ignore())
                .ReverseMap();
        }
    }
}
