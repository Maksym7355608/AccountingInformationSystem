namespace AccountingInformationSystem.Data.Entities
{
    public class User : Employee
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
