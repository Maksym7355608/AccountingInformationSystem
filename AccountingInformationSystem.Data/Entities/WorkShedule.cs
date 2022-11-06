namespace AccountingInformationSystem.Data.Entities
{
    public class WorkShedule
    {
        public int Id { get; set; }
        /// <summary>
        /// Табельний номер
        /// </summary>
        public long IdentificationNumber { get; set; }
        /// <summary>
        /// Період типу yyyymm
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// Робочий графік робітника
        /// </summary>
        public List<Shedule> Shedule { get; set; }
    }
}
