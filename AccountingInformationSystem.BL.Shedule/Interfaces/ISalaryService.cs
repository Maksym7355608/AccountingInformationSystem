using AccountingInformationSystem.Finances.DataModels;
using AccountingInformationSystem.Templates.Abstractions.CreateObject;

namespace AccountingInformationSystem.Finances.Interfaces
{
    public interface ISalaryService
    {
        FinanceDataModel CalculateSalary(BaseCreateObject createObject);
    }
}
