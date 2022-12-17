using AccountingInformationSystem.Templates.Abstractions.CreateObject;

namespace AccountingInformationSystem.Shedules.CreateObjects
{
    public class SheduleCreateObject : BaseCreateObject
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string OrganizationDepartament { get; set; }
        public string OrganizationUnit { get; set; }
    }
}
