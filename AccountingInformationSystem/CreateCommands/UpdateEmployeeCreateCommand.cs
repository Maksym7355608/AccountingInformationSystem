using AccountingInformationSystem.Data.Entities;

namespace AccountingInformationSystem.CreateCommands
{
    public class UpdateEmployeeCreateCommand
    {
        /// <summary>
        /// Ім'я
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Прізвище
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Чи одружений
        /// </summary>
        public bool MarriedStatus { get; set; }
        /// <summary>
        /// Діти
        /// </summary>
        public int? Kids { get; set; }
        /// <summary>
        /// Позиція
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Відділ
        /// </summary>
        public string Departament { get; set; }
        /// <summary>
        /// Підрозділ
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// Заробітня плата
        /// </summary>
        public decimal Salary { get; set; }
        /// <summary>
        /// Податкова соціальна пільга
        /// </summary>
        public EBenefits Benefits { get; set; }
        /// <summary>
        /// Дата переведення на іншу посаду
        /// </summary>
        public DateTime? TransferDate { get; set; }
    }
}
