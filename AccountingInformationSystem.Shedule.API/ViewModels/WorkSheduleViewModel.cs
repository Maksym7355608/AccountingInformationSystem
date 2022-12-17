using AccountingInformationSystem.Data.Entities;

namespace AccountingInformationSystem.Shedules.API.ViewModels
{
    public class WorkSheduleViewModel
    {
        /// <summary>
        /// identity employee code
        /// </summary>
        public long Id { get; set; }
        public string FullName { get; set; }
        public List<SheduleViewModel> Shedule { get; set; }
    }

    public class SheduleViewModel
    {
        public DateTime Date { get; set; }
        public decimal Time { get; set; }
        public EDayType DayType { get; set; }
    }
}
