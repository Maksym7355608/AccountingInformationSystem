using AccountingInformationSystem.Data.EF;
using AccountingInformationSystem.Data.Entities;
using AccountingInformationSystem.Finances.CreateObject;
using AccountingInformationSystem.Finances.DataLoaders;
using AccountingInformationSystem.Finances.DataModels;
using AccountingInformationSystem.Finances.Interfaces;
using AccountingInformationSystem.Templates.Abstractions.CreateObject;
using AccountingInformationSystem.Templates.Helpers;
using AutoMapper;

namespace AccountingInformationSystem.Finances.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly AccountingInformationSystemContext _sqlContext;
        private readonly IMapper _mapper;
        private static FinancesDataLoader _dataLoader;

        public SalaryService(AccountingInformationSystemContext context, IMapper mapper)
        {
            _sqlContext = context;
            _mapper = mapper;
        }

        #region Main
        public IEnumerable<FinanceDataModel> CalculatePayoutsByFilter(BaseCreateObject createObject)
        {
            var filter = (FinancesCreateObject)createObject;

            _dataLoader ??= LoadCasheData(filter);

            var periods = ExtensionHelper.GetPeriodsArray(filter.PeriodFrom, filter.PeriodTo);

            foreach (var employee in _dataLoader.Employees)
            {
                var shedules = employee.Value.WorkShedules.Where(x => x.Period >= filter.PeriodFrom && (filter.PeriodTo >= x.Period))
                    .SelectMany(x => x.Shedule).ToList();
                yield return new FinanceDataModel
                {
                    EmployeeId = employee.Key,
                    PeriodFrom = filter.PeriodFrom,
                    PeriodTo = filter.PeriodTo,
                    FullName = employee.Value.FullName,
                    WorkPayout = GetPayout(employee.Value.Salary, periods, GetTimeByDayTypes(shedules, EDayType.Work)),
                    SickPayout = GetPayout(employee.Value.Salary, periods, GetTimeByDayTypes(shedules, EDayType.Sick)),
                    VacationPayout = GetPayout(employee.Value.Salary, periods, GetTimeByDayTypes(shedules, EDayType.Vacation)),
                    DayOffs = shedules.Count(x => x.DayType == EDayType.DayOff),
                    Benefit = GetBenefits(employee.Value)
                };
            }
        }

        private decimal? GetBenefits(EmployeeDataModel employee)
        {
            return employee.Benefits switch
            {
                EBenefits.Kids => CalculateKidsBenefits(employee.Salary, employee.Kids.Value, 100),

                EBenefits.Widow
                or EBenefits.Chernobyl
                or EBenefits.KidsWithDisability => CalculateKidsBenefits(employee.Salary, employee.Kids ?? 0, 150),

                EBenefits.FirstDisability
                or EBenefits.SecondDisability
                or EBenefits.Student
                or EBenefits.AssignedLifetimeScholarship
                or EBenefits.MilitaryAfterWWII => CalculateBenefits(employee.Salary, 150),

                EBenefits.HeroTitul
                or EBenefits.FourMedalForCourage
                or EBenefits.MilitaryInWWII
                or EBenefits.PrisonersOfConcentrationCamps
                or EBenefits.PersonRecognizedAsRepressedOrRehabilitated => CalculateBenefits(employee.Salary, 200),

                _ => null,
            };
        }

        private decimal CalculateBenefits(decimal salary, int percent)
        {
            if (salary > ValueHelper.MinimalSalary)
                return 0;
            else
                return (ValueHelper.TaxSocialBenefit * ((decimal)percent / 100));
        }

        private decimal CalculateKidsBenefits(decimal salary, int kids, int percent)
        {
            if (salary > (ValueHelper.MinimalSalary * kids))
                return 0;
            else
                return (ValueHelper.TaxSocialBenefit * ((decimal)percent / 100)) * kids;
        }

        private Payout GetPayout(decimal salary, int[] periods, decimal hours)
        {
            if (hours == 0)
                return new Payout();
            var payout = 0m;
            foreach (var period in periods)
            {
                DateTime periodDateTime = period.ToDateTime();

                var workDaysAtMonth = WeekDaysInMonth(periodDateTime.Year, periodDateTime.Month);
                payout += CalculatePayout(hours, workDaysAtMonth, salary);
            }
            return new Payout
            {
                Hours = hours,
                PayoutWithoutTaxes = payout,
                Tax = payout * ValueHelper.TAX,
                ArmyTax = payout * ValueHelper.MTAX,
            };
        }

        #endregion

        #region helpers methods

        private decimal CalculatePayout(decimal workHours, int workDaysAtMonth, decimal salary)
        {
            return (workDaysAtMonth * ValueHelper.NormalDayHours / salary) * workHours;
        }

        private int WeekDaysInMonth(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            List<DateTime> dates = new List<DateTime>();
            for (int i = 1; i <= days; i++)
                dates.Add(new DateTime(year, month, i));

            int weekDays = dates.Where(d => d.DayOfWeek > DayOfWeek.Sunday
                    & d.DayOfWeek < DayOfWeek.Saturday).Count();
            return weekDays;
        }

        private decimal GetTimeByDayTypes(List<SheduleDataModel> shedules, EDayType dayType)
        {
            return shedules.Where(x => x.DayType == dayType).Sum(x => x.Time);
        }

        #endregion

        #region Load Cashe Data
        private Dictionary<long, EmployeeDataModel> GetEmployeesByFilters(FinancesCreateObject filter)
        {
            var employees = _sqlContext.Employees.Where(GetFilterValue(filter));
            if (!employees.Any())
                throw new Exception("Employees is not found");
            var mappedModel = _mapper.Map<IEnumerable<EmployeeDataModel>>(employees);
            return mappedModel.ToDictionary(k => k.Id, v => v);
        }

        private Func<Employee, bool> GetFilterValue(FinancesCreateObject filter)
        {
            return (x => (string.IsNullOrEmpty(filter.OrganizationDepartament) && filter.OrganizationDepartament == x.Departament) &&
                (string.IsNullOrEmpty(filter.OrganizationUnit) && filter.OrganizationUnit == x.Unit) &&
                (filter.EmployeeId.HasValue && filter.EmployeeId.Value == x.IdentificationNumber));
        }

        private FinancesDataLoader LoadCasheData(FinancesCreateObject filter)
        {
            var loadedData = new FinancesDataLoader();
            loadedData.Employees = GetEmployeesByFilters(filter);
            return loadedData;
        }
        #endregion
    }
}
