using AccountingInformationSystem.Data.Entities;

namespace AccountingInformationSystem.Shedules.DataModels
{
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
        /// <summary>
        /// identity employee code
        /// </summary>
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Time { get; set; }
        public EDayType DayType { get; set; }
    }
}
