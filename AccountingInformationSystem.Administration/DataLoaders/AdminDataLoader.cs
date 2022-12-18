using AccountingInformationSystem.Administration.DataModels;
using AccountingInformationSystem.Templates.Abstractions.DataLoaders;

namespace AccountingInformationSystem.Administration.DataLoaders
{
    public class AdminDataLoader : BaseDataLoader
    {
        public List<EmployeeDataModel> Employees { get; set; }
        public List<UserDataModel> Users { get; set; }
    }
}
