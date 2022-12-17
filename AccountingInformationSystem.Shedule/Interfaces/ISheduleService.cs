using AccountingInformationSystem.Shedules.CreateObjects;
using AccountingInformationSystem.Shedules.DataModels;

namespace AccountingInformationSystem.Shedules.Interfaces
{
    public interface ISheduleService
    {
        IEnumerable<WorkSheduleDataModel> GetWorkShedulesByFilters(SheduleCreateObject filter);
        Task AddShedulesAsync(IEnumerable<WorkSheduleDataModel> shedules);
        Task UpdateShedulesAsync(IEnumerable<WorkSheduleDataModel> shedules);
    }
}
