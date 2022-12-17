namespace AccountingInformationSystem.Shedules.API.CreateCommands
{
    public class SheduleCreateCommand
    {
        public string OrganizationDepartament { get; set; }
        public string OrganizationUnit { get; set; }
        public long? EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
