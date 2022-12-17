namespace AccountingInformationSystem.Templates.Helpers
{
    public static class ExtensionHelper
    {
        public static int ToPeriod(this DateTime dateTime)
        {
            return dateTime.Year * 100 + dateTime.Month;
        }

        public static DateTime ToDateTime(this int period)
        {
            var year = period / 100;
            var month = period % 100;
            var day = 1;
            return new DateTime(year, month, day);
        }

        public static int NextPeriod(this int period)
        {
            period++;
            if (period % 100 > 12)
            {
                var newPeriod = (period / 100 + 1) + 1;
                return (int)newPeriod;
            }
            else return period;
        }

        public static int PreviousPeriod(this int period)
        {
            period--;
            if (period % 100 == 0)
            {
                var newPeriod = (period / 100 - 1) + 12;
                return (int)newPeriod;
            }
            else return period;
        }

        public static bool PeriodFilter(int periodFrom1, int periodFrom2, int periodTo1, int periodTo2) =>
            periodFrom1 == periodFrom2 && periodTo1 == periodTo2;

        public static int[] GetPeriodsArray(int periodFrom, int periodTo)
        {
            var periods = new List<int>();
            for (var currentPeriod = periodFrom; currentPeriod <= periodTo; currentPeriod = currentPeriod.NextPeriod())
            {
                periods.Add(currentPeriod);
            }
            return periods.ToArray();
        }
        
    }
}
