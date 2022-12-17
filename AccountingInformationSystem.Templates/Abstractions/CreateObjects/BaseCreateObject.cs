using AccountingInformationSystem.Templates.Helpers;

namespace AccountingInformationSystem.Templates.Abstractions.CreateObject
{
    public class BaseCreateObject
    {
        public long? EmployeeId { get; set; }
        public int PeriodFrom { get; set; }
        public int PeriodTo { get; set; } = DateTime.Now.ToPeriod();
    }
}
