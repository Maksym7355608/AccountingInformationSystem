using AccountingInformationSystem.Data.Entities;

namespace AccountingInformationSystem.CreateCommands
{
    public class RegistrationCreateCommand : AddEmployeeCreateCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
