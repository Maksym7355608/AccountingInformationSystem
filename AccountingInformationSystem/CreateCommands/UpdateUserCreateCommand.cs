namespace AccountingInformationSystem.CreateCommands
{
    public class UpdateUserCreateCommand : UpdateEmployeeCreateCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
