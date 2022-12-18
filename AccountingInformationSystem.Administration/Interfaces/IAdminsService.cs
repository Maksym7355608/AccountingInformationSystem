using AccountingInformationSystem.Administration.DataModels;
using AccountingInformationSystem.Templates.Abstractions.CreateObject;

namespace AccountingInformationSystem.Administration.Interfaces
{
    public interface IAdminsService
    {
        IEnumerable<EmployeeDataModel> GetEmployees();
        IEnumerable<EmployeeDataModel> GetEmployeesByFilters(BaseCreateObject createObject);
        EmployeeDataModel GetEmployeeById(long id);
        Task AddNewEmployeeAsync(EmployeeDataModel model);
        Task DeleteEmployeeAsync(long id);
        Task UpdateEmployeeAsync(EmployeeDataModel model);
    }
}
