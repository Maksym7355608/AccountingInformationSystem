using AccountingInformationSystem.Administration.CreateObjects;
using AccountingInformationSystem.Administration.DataModels;
using AccountingInformationSystem.Administration.Infrastructure.Exceptions;
using AccountingInformationSystem.Administration.Interfaces;
using AccountingInformationSystem.Data.EF;
using AccountingInformationSystem.Data.Entities;
using AccountingInformationSystem.Templates.Abstractions.CreateObject;
using AccountingInformationSystem.Templates.Helpers;
using AutoMapper;

namespace AccountingInformationSystem.Administration.Services
{
    public class AdminService : IAdminsService
    {
        private readonly AccountingInformationSystemContext _sqlContext;
        private readonly IMapper _mapper;

        public AdminService(AccountingInformationSystemContext context, IMapper mapper)
        {
            _sqlContext = context;
            _mapper = mapper;
        }

        public async Task AddNewEmployeeAsync(EmployeeDataModel model)
        {
            ValidateInput(model);

            var mappedModel = _mapper.Map<Employee>(model);
            await _sqlContext.Employees.AddAsync(mappedModel);
            await _sqlContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(long id)
        {
            var employee = _sqlContext.Employees.FirstOrDefault(x => x.IdentificationNumber == id);
            if (employee == default)
                throw new KeyNotFoundException($"User with id: {id} not found");

            _sqlContext.Employees.Remove(employee);
            await _sqlContext.SaveChangesAsync();
        }

        public EmployeeDataModel GetEmployeeById(long id)
        {
            var employee = _sqlContext.Employees.FirstOrDefault(x => x.IdentificationNumber == id);
            if (employee == default)
                throw new KeyNotFoundException($"User with id: {id} not found");
            return _mapper.Map<EmployeeDataModel>(employee);
        }

        public IEnumerable<EmployeeDataModel> GetEmployees()
        {
            return _mapper.Map<IEnumerable<EmployeeDataModel>>(_sqlContext.Employees);
        }

        public IEnumerable<EmployeeDataModel> GetEmployeesByFilters(BaseCreateObject createObject)
        {
            var filter = (AdministrationCreateObject)createObject;

            var employees = _sqlContext.Employees.Where(GetEmployeesFilter(filter));
            return _mapper.Map<IEnumerable<EmployeeDataModel>>(employees);
        }

        private Func<Employee, bool> GetEmployeesFilter(AdministrationCreateObject filter)
        {
            return employee => 
            (string.IsNullOrEmpty(filter.OrganizationDepartament) || employee.Departament == filter.OrganizationDepartament) &&
            (string.IsNullOrEmpty(filter.OrganizationUnit) || employee.Unit == filter.OrganizationUnit) &&
            (string.IsNullOrEmpty(filter.Position) || employee.Position == filter.Position);
        }

        public async Task UpdateEmployeeAsync(EmployeeDataModel model)
        {
            ValidateInput(model);

            var mappedModel = _mapper.Map<Employee>(model);
            _sqlContext.Employees.Update(mappedModel);
            await _sqlContext.SaveChangesAsync();
        }

        #region Validation registration
        private void ValidateInput(EmployeeDataModel user)
        {
            if (CheckInvalidName(user.Name, user.Surname, user.Fatherly))
                throw new InvalidInputException("You input inccorrect name/surname/fatherly data");
            if (CheckDepartament(user.Departament))
                throw new InvalidInputException("Departament exist");
            if (CheckUnit(user.Departament, user.Unit))
                throw new InvalidInputException("Unit exist");
            if (CheckPosition(user.Unit, user.Position))
                throw new InvalidInputException("Position exist");
        }

        private bool CheckInvalidName(string name, string surname, string fatherly)
        {
            return StringInputChecker(name) || StringInputChecker(surname) || StringInputChecker(fatherly);
        }

        private bool StringInputChecker(string str)
        {
            return string.IsNullOrEmpty(str) || char.IsLower(str[0]) || str.Any(x => char.IsDigit(x));
        }

        private bool CheckDepartament(string departament)
        {
            return !ValueHelper.Departaments.Any(x => x.name == departament);
        }

        private bool CheckUnit(string departament, string unit)
        {
            var departamentId = ValueHelper.Departaments.FirstOrDefault(x => x.name == departament).id;
            return !ValueHelper.Units.Where(x => x.departamentId == departamentId).Any(x => x.name == unit);
        }

        private bool CheckPosition(string unit, string position)
        {
            var unitId = ValueHelper.Units.FirstOrDefault(x => x.name == unit).id;
            return !ValueHelper.Units.Where(x => x.id == unitId).Any(x => x.name == position);
        }
        #endregion

    }
}
