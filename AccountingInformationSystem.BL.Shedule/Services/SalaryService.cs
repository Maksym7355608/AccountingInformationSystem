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

        public SalaryService(AccountingInformationSystemContext context, IMapper mapper)
        {
            _sqlContext = context;
            _mapper = mapper;
        }

        public FinanceDataModel CalculateSalary(BaseCreateObject createObject)
        {
            var filter = (FinancesCreateObject)createObject;

            var dataLoader = LoadCasheData(filter);
            var workShedule = dataLoader.Employee.WorkShedules.FirstOrDefault(x => x.Period == filter.Period);
            return new FinanceDataModel
            {
                EmployeeId = filter.EmployeeId,
                ReportPeriod = filter.Period.Value,
                FullName = dataLoader.Employee.FullName,
                WorkPayout = GetPayout(dataLoader.Employee.Salary, filter.Period.Value, workShedule.Shedule.Where(x => x.DayType == EDayType.Work).Sum(x => x.Time)),
                SickPayout = GetPayout(dataLoader.Employee.Salary, filter.Period.Value, workShedule.Shedule.Where(x => x.DayType == EDayType.Sick).Sum(x => x.Time)),
                VacationPayout = GetPayout(dataLoader.Employee.Salary, filter.Period.Value, workShedule.Shedule.Where(x => x.DayType == EDayType.Vacation).Sum(x => x.Time)),
                DayOffs = workShedule.Shedule.Where(x => x.DayType == EDayType.DayOff).Count(),
                Benefit = GetBenefits(dataLoader.Employee)
            };
        }

        private decimal? GetBenefits(EmployeeDataModel employee)
        {
            return employee.Benefits switch
            {
                EBenefits.Kids => CalculateKidsBenefits(employee.Salary, employee.Kids.Value, 100),
                EBenefits.Widow
                or EBenefits.Chernobyl
                or EBenefits.KidsWithDisability => CalculateKidsBenefits(employee.Salary, employee.Kids.Value, 150),
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

        private Payout GetPayout(decimal salary, int period, decimal hours)
        {
            if (hours == 0)
                return new Payout();

            DateTime periodDateTime = period.ToDateTime();

            var workDaysAtMonth = WeekDaysInMonth(periodDateTime.Year, periodDateTime.Month);
            var payout = CalculatePayout(hours, workDaysAtMonth, salary);
            return new Payout
            {
                Hours = hours,
                PayoutWithoutTaxes = payout,
                Tax = payout * ValueHelper.TAX,
                ArmyTax = payout * ValueHelper.MTAX,
            };
        }

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

        #region Load Cashe Data
        private EmployeeDataModel GetEmployee(long identificationNumber)
        {
            var employee = _mapper.Map<Employee, EmployeeDataModel>(_sqlContext.Employees
                .FirstOrDefault(emp => emp.IdentificationNumber == identificationNumber));
            if (employee == null)
                throw new Exception("Employee is not found");
            return employee;
        }

        private FinancesDataLoader LoadCasheData(BaseCreateObject createObject)
        {
            return new FinancesDataLoader
            {
                Id = createObject.EmployeeId,
                Employee = GetEmployee(createObject.EmployeeId)
            };
        }
        #endregion
    }
}
