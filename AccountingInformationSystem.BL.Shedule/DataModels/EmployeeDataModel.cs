using AccountingInformationSystem.Data.Entities;

namespace AccountingInformationSystem.Finances.DataModels
{
    public class EmployeeDataModel
    {
        /// <summary>
        /// identity employee code
        /// </summary>
        public long Id { get; set; }
        public string FullName { get; set; }
        public decimal Salary { get; set; }
        public int? Kids { get; set; }
        public EBenefits Benefits { get; set; }
        public List<WorkSheduleDataModel> WorkShedules { get; set; }
    }

    public class WorkSheduleDataModel
    {
        /// <summary>
        /// identity employee code
        /// </summary>
        public long Id { get; set; }
        public int Period { get; set; }
        public List<SheduleDataModel> Shedule { get; set; }
    }

    public class SheduleDataModel
    {
        public DateTime Date { get; set; }
        public decimal Time { get; set; }
        public EDayType DayType { get; set; }
    }
}
