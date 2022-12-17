using AccountingInformationSystem.Data.Entities;
using AccountingInformationSystem.Shedules.DataModels;
using AutoMapper;

namespace AccountingInformationSystem.Shedules.Infrastructure.AutoMappers
{
    public class SheduleAutoMapperProfile : Profile
    {
        public SheduleAutoMapperProfile()
        {
            CreateMap<WorkShedule, WorkSheduleDataModel>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.IdentificationNumber))
                .ForMember(x => x.Shedule, y => y.MapFrom(z => z.Shedule))
                .ReverseMap();

            CreateMap<Shedule, SheduleDataModel>()
                .ReverseMap();
        }
    }
}
