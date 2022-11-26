using AccountingInformationSystem.Shedules.CreateObjects;
using AccountingInformationSystem.Shedules.DataModels;

namespace AccountingInformationSystem.Shedules.Interfaces
{
    public interface ISheduleService
    {
        IEnumerable<WorkSheduleDataModel> GetWorkShedulesByFilters(SheduleCreateObject filter);
        Task UploadWorkDaysAsync(IEnumerable<SheduleDataModel> shedules);
        Task UpdateWorkDaysAsync(IEnumerable<SheduleDataModel> shedules);
    }
}
