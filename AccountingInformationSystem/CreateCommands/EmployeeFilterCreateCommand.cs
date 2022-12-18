namespace AccountingInformationSystem.CreateCommands
{
    public class EmployeeFilterCreateCommand
    {
        public long? EmployeeId { get; set; }
        public string OrganizationDepartament { get; set; }
        public string OrganizationUnit { get; set; }
        public string Position { get; set; }
    }
}
