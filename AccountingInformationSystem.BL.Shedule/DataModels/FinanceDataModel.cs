namespace AccountingInformationSystem.Finances.DataModels
{
    public class FinanceDataModel
    {
        public long EmployeeId { get; set; }
        public string FullName { get; set; }
        public int PeriodFrom { get; set; }
        public int? PeriodTo { get; set; }
        public Payout WorkPayout { get; set; }
        public Payout SickPayout { get; set; }
        public Payout VacationPayout { get; set; }
        public int DayOffs { get; set; }
        public decimal? Benefit { get; set; }
        public decimal SummaryPayout => Benefit.HasValue ?
            WorkPayout.SummaryPayout + SickPayout.SummaryPayout + VacationPayout.SummaryPayout + Benefit.Value :
            WorkPayout.SummaryPayout + SickPayout.SummaryPayout + VacationPayout.SummaryPayout;
    }

    public class Payout
    {
        public decimal Hours { get; set; }
        public decimal PayoutWithoutTaxes { get; set; }
        public decimal Tax { get; set; }
        public decimal ArmyTax { get; set; }
        public decimal SummaryPayout => PayoutWithoutTaxes - Tax - ArmyTax;
    }
}
