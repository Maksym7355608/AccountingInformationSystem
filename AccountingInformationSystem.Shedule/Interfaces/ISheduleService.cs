using AccountingInformationSystem.Shedules.CreateObjects;
using AccountingInformationSystem.Shedules.DataModels;

namespace AccountingInformationSystem.Shedules.Interfaces
{
    public interface ISheduleService
    {
        IEnumerable<WorkSheduleDataModel> GetWorkShedulesByFilters(SheduleCreateObject filter);
        Task UploadWorkDaysAsync(IEnumerable<WorkSheduleDataModel> shedules);
        Task UpdateWorkDaysAsync(IEnumerable<WorkSheduleDataModel> shedules);
    }
}
