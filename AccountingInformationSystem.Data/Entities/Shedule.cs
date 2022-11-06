using System.ComponentModel;

namespace AccountingInformationSystem.Data.Entities
{
    public class Shedule
    {
        public int Id { get; set; }
        /// <summary>
        /// Ід робочого графіку
        /// </summary>
        public int WorkSheduleId { get; set; }
        /// <summary>
        /// Використаний час
        /// </summary>
        public decimal Time { get; set; }
        /// <summary>
        /// Тип дня - EDayType
        /// </summary>
        public EDayType DayType { get; set; }
    }

    public enum EDayType
    {
        [Description("Work day")]
        Work = 1,
        [Description("Day off")]
        DayOff = 2,
        [Description("Vacation day")]
        Vacation = 3,
        [Description("Day off due to sick")]
        Sick = 4
    }
}