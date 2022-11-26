using AccountingInformationSystem.Data.EF;
using AccountingInformationSystem.Data.Entities;
using AccountingInformationSystem.Shedules.CreateObjects;
using AccountingInformationSystem.Shedules.DataLoaders;
using AccountingInformationSystem.Shedules.DataModels;
using AccountingInformationSystem.Shedules.Interfaces;
using AccountingInformationSystem.Templates.Helpers;
using AutoMapper;
using System.Linq;

namespace AccountingInformationSystem.Shedules.Services
{
    public class SheduleService : ISheduleService
    {
        private readonly AccountingInformationSystemContext _sqlContext;
        private readonly IMapper _mapper;
        private static SheduleDataLoader _dataLoader;

        public SheduleService(AccountingInformationSystemContext context, IMapper mapper)
        {
            _sqlContext = context;
            _mapper = mapper;
        }

        public IEnumerable<WorkSheduleDataModel> GetWorkShedulesByFilters(SheduleCreateObject filter)
        {
            if (_dataLoader == null)
                _dataLoader = LoadCacheData(filter);
            return _dataLoader.WorkShedule;
        }

        public async Task UpdateWorkDaysAsync(IEnumerable<SheduleDataModel> shedules)
        {
            var checker = shedules.Where(shedule => !_mapper.Map<List<SheduleDataModel>>(_dataLoader.WorkShedule.Select(sh => sh.Shedule))
                                        .Contains(shedule));
            if (checker.Any())
                await UploadWorkDaysAsync(shedules);

            _sqlContext.Shedules.UpdateRange(_mapper.Map<IEnumerable<Shedule>>(shedules));
            await _sqlContext.SaveChangesAsync();
        }

        public async Task UploadWorkDaysAsync(IEnumerable<SheduleDataModel> shedules)
        {
            var checker = shedules.Where(shedule => !_mapper.Map<List<SheduleDataModel>>(_dataLoader.WorkShedule.Select(sh => sh.Shedule))
                                        .Contains(shedule));
            if (checker.Any())
                await UpdateWorkDaysAsync(shedules);

            _sqlContext.Shedules.AddRange(_mapper.Map<IEnumerable<Shedule>>(shedules));
            await _sqlContext.SaveChangesAsync();
        }

        private SheduleDataLoader LoadCacheData(SheduleCreateObject filter)
        {
            return new SheduleDataLoader
            {
                Id = filter.EmployeeId,
                WorkShedule = GetWorkShedules(filter)
            };
        }

        private List<WorkSheduleDataModel> GetWorkShedules(SheduleCreateObject filter)
        {
            var employeesIds = _sqlContext.Employees.Where(employee => GetEmployeeFilter(employee, filter))
                .Select(employees => employees.IdentificationNumber);
            var shedule = _sqlContext.WorkShedules.Where(shedule => GetSheduleFilter(shedule, employeesIds, filter));
   
            if (shedule.Any())
                return _mapper.Map<List<WorkSheduleDataModel>>(shedule);
            else
                return new List<WorkSheduleDataModel>();
        }

        private bool GetSheduleFilter(WorkShedule shedule, IEnumerable<long> ids, SheduleCreateObject filter) => 
            ids.Contains(shedule.IdentificationNumber) &&
            shedule.Period >= filter.DateFrom.ToPeriod() &&
            shedule.Period <= filter.DateTo.ToPeriod();

        private bool GetEmployeeFilter(Employee employee, SheduleCreateObject filter) =>
            (string.IsNullOrEmpty(filter.OrganizationDepartament) || employee.Departament == filter.OrganizationDepartament) &&
            (string.IsNullOrEmpty(filter.OrganizationUnit) || employee.Unit == filter.OrganizationUnit) &&
            (string.IsNullOrEmpty(filter.Position) || employee.Position == filter.Position);
    }
}

