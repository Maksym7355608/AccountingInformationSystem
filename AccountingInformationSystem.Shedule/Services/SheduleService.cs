using AccountingInformationSystem.Data.EF;
using AccountingInformationSystem.Data.Entities;
using AccountingInformationSystem.Shedules.CreateObjects;
using AccountingInformationSystem.Shedules.DataLoaders;
using AccountingInformationSystem.Shedules.DataModels;
using AccountingInformationSystem.Shedules.Interfaces;
using AccountingInformationSystem.Templates.Helpers;
using AutoMapper;

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
            return _dataLoader.WorkShedules;
        }

        private Func<WorkSheduleDataModel, bool> GetContainsSheduleFilter(IEnumerable<WorkSheduleDataModel> shedules)
        {
            return x =>
                shedules.Select(sh => sh.Period).Contains(x.Period);
        }

        public async Task UpdateShedulesAsync(IEnumerable<WorkSheduleDataModel> shedules)
        {
            var mappedModel = _mapper.Map<IEnumerable<WorkShedule>>(shedules);
            var containsPeriodsShedule = _dataLoader.WorkShedules.Where(GetContainsSheduleFilter(shedules));
            var notContainsPeriodsShedule = _dataLoader.WorkShedules.Where(x => !containsPeriodsShedule.Contains(x));
            if (notContainsPeriodsShedule.Any())
                await AddShedulesAsync(notContainsPeriodsShedule);

            _sqlContext.WorkShedules.UpdateRange(_mapper.Map<IEnumerable<WorkShedule>>(containsPeriodsShedule));
            await _sqlContext.SaveChangesAsync();
        }

        public async Task AddShedulesAsync(IEnumerable<WorkSheduleDataModel> shedules)
        {
            var containsPeriodsShedule = _dataLoader.WorkShedules.Where(GetContainsSheduleFilter(shedules));
            var notContainsPeriodsShedule = _dataLoader.WorkShedules.Where(x => !containsPeriodsShedule.Contains(x));
            if (containsPeriodsShedule.Any())
                await UpdateShedulesAsync(containsPeriodsShedule);

            _sqlContext.Shedules.AddRange(_mapper.Map<IEnumerable<Shedule>>(notContainsPeriodsShedule));
            await _sqlContext.SaveChangesAsync();
        }

        private SheduleDataLoader LoadCacheData(SheduleCreateObject filter)
        {
            var loadedData = new SheduleDataLoader();
            loadedData.WorkShedules = GetWorkShedules(filter);
            return loadedData;
        }

        private List<WorkSheduleDataModel> GetWorkShedules(SheduleCreateObject filter)
        {
            var employeesIds = _sqlContext.Employees.Where(employee => GetEmployeeFilter(employee, filter))
                .Select(employees => employees.IdentificationNumber);
            var shedule = _sqlContext.WorkShedules.Where(GetSheduleFilter(employeesIds, filter))
                    .ToList();

            if (shedule.Any())
                return _mapper.Map<List<WorkSheduleDataModel>>(shedule);
            else
                return new List<WorkSheduleDataModel>();
        }

        private Func<WorkShedule, bool> GetSheduleFilter(IEnumerable<long> ids, SheduleCreateObject filter)
        {
            return (shedule =>
                ids.Contains(shedule.IdentificationNumber) &&
                shedule.Period >= filter.DateFrom.ToPeriod() &&
                shedule.Period <= filter.DateTo.ToPeriod());
        }

        private bool GetEmployeeFilter(Employee employee, SheduleCreateObject filter) =>
            (string.IsNullOrEmpty(filter.OrganizationDepartament) || employee.Departament == filter.OrganizationDepartament) &&
            (string.IsNullOrEmpty(filter.OrganizationUnit) || employee.Unit == filter.OrganizationUnit);
    }
}

