using AccountingInformationSystem.Finances.DataModels;
using AccountingInformationSystem.Templates.Abstractions.CreateObject;

namespace AccountingInformationSystem.Finances.Interfaces
{
    public interface ISalaryService
    {
        IEnumerable<FinanceDataModel> CalculatePayoutsByFilter(BaseCreateObject createObject);
    }
}
