namespace AccountingInformationSystem.Finances.API.CreateCommands
{
    public class CalculationPayoutCreateCommand
    {
        public string Departament { get; set; }
        public string Unit { get; set; }
        public long? EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
