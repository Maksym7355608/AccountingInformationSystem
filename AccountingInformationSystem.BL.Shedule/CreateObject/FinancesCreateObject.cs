using AccountingInformationSystem.Templates.Abstractions.CreateObject;

namespace AccountingInformationSystem.Finances.CreateObject
{
    public class FinancesCreateObject : BaseCreateObject
    {
        public string Departament { get; set; }
        public string Unit { get; set; }
    }
}
