using AccountingInformationSystem.Data.Entities;
using AccountingInformationSystem.Finances.DataModels;
using AutoMapper;

namespace AccountingInformationSystem.Finances.Infrastructure.AutoMapper
{
    public class FinancesAutoMapperProfile : Profile
    {
        public FinancesAutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDataModel>()
                .ForMember(x => x.WorkShedules, y => y.MapFrom(wsh => wsh.WorkShedules))
                .ForMember(x => x.FullName, y => y.MapFrom(fn => $"{fn.Name} {fn.Surname} {fn.Fatherly}"))
                .ReverseMap();

            CreateMap<WorkShedule, WorkSheduleDataModel>()
                .ForMember(x => x.Shedule, y => y.MapFrom(sh => sh.Shedule))
                .ReverseMap();

            CreateMap<Shedule, SheduleDataModel>()
                .ReverseMap();
        }
    }
}
