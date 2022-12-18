using AccountingInformationSystem.Shedules.API.ViewModels;
using AccountingInformationSystem.Shedules.DataModels;
using AutoMapper;

namespace AccountingInformationSystem.Shedules.API.Infrastructure.AutoMappers
{
    public class SheduleMapperProfile: Profile
    {
        public SheduleMapperProfile()
        {
            CreateMap<WorkSheduleDataModel, WorkSheduleViewModel>()
                .ReverseMap();
            CreateMap<SheduleDataModel, SheduleViewModel>()
                .ReverseMap();
        }
    }
}
