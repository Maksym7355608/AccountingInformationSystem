using AccountingInformationSystem.Finances.API.ViewModels;
using AccountingInformationSystem.Finances.DataModels;
using AutoMapper;

namespace AccountingInformationSystem.Finances.API.Infrastructure.AutoMappers
{
    public class FinanceMapperProfile : Profile
    {
        public FinanceMapperProfile()
        {
            CreateMap<FinanceDataModel, FinanceViewModel>()
                .ReverseMap();
        }
    }
}
