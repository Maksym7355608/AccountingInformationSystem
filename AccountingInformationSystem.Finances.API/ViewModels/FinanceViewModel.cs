namespace AccountingInformationSystem.Finances.API.ViewModels
{
    public class FinanceViewModel
    {
        public long EmployeeId { get; set; }
        public string FullName { get; set; }
        public int ReportPeriod { get; set; }
        public PayoutViewModel WorkPayout { get; set; }
        public PayoutViewModel SickPayout { get; set; }
        public PayoutViewModel VacationPayout { get; set; }
        public int? DayOffs { get; set; }
        public decimal? Benefit { get; set; }
        public decimal SummaryPayout => Benefit.HasValue ?
            WorkPayout.SummaryPayout + SickPayout.SummaryPayout + VacationPayout.SummaryPayout + Benefit.Value :
            WorkPayout.SummaryPayout + SickPayout.SummaryPayout + VacationPayout.SummaryPayout;
    }

    public class PayoutViewModel
    {
        public decimal Hours { get; set; }
        public decimal PayoutWithoutTaxes { get; set; }
        public decimal Tax { get; set; }
        public decimal ArmyTax { get; set; }
        public decimal SummaryPayout => PayoutWithoutTaxes - Tax - ArmyTax;
    }
}
