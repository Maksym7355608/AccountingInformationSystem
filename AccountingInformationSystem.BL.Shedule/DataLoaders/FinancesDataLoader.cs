using AccountingInformationSystem.Finances.DataModels;
using AccountingInformationSystem.Templates.Abstractions.DataLoaders;

namespace AccountingInformationSystem.Finances.DataLoaders
{
    public class FinancesDataLoader : BaseDataLoader
    {
        public EmployeeDataModel Employee { get; set; }
    }
}
