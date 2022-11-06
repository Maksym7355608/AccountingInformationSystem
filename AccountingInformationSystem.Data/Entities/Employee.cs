namespace AccountingInformationSystem.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        /// <summary>
        /// Табельний номер
        /// </summary>
        public long IdentificationNumber { get; set; }
        /// <summary>
        /// Ім'я
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Прізвище
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// По-батькові
        /// </summary>
        public string Fatherly { get; set; }
        /// <summary>
        /// День народження
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// Чи одружений
        /// </summary>
        public bool MarriedStatus { get; set; }
        /// <summary>
        /// Дата найняття
        /// </summary>
        public DateTime EmploymentDate { get; set; }
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
        /// Робочий графік
        /// </summary>
        public List<WorkShedule> WorkShedules { get; set; }
    }
}
