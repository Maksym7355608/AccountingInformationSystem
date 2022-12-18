using AccountingInformationSystem.Templates.Abstractions.CreateObject;

namespace AccountingInformationSystem.Administration.CreateObjects
{
    public class AdministrationCreateObject : BaseCreateObject
    {
        public string OrganizationDepartament { get; set; }
        public string OrganizationUnit { get; set; }
        public string Position { get; set; }
    }
}
