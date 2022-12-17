using AccountingInformationSystem.Templates.Abstractions.CreateObject;

namespace AccountingInformationSystem.Finances.CreateObject
{
    public class FinancesCreateObject : BaseCreateObject
    {
        public string OrganizationDepartament { get; set; }
        public string OrganizationUnit { get; set; }
    }
}
