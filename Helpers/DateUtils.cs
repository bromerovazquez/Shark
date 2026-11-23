using System.Globalization;

namespace Drim.Interfaces.MVC.Helpers
{
    public class DateUtils
    {

        public static DateTime FirstDateOfWeek(string week_format_1)
        {
            Int32 week_key = 0;
            if (week_format_1.Trim().Length != 6 || !Int32.TryParse(week_format_1, out week_key))
            {
                throw new ArgumentException(String.Format("'{0}' is invalid week format", week_format_1));
            }

            int year = week_key / 100;
            int weekOfYear = week_key % 100;

            return FirstDateOfWeek(year, weekOfYear);
        }

        public static string WeekId(DateTime date)
        {
            int wk = 0;
            int year = 0;

            DateUtils.GetWeekNumber(date, out year, out wk);

            return String.Format("{0}-{1:00}", year, wk);
        }

        ///// <summary>
        ///// Returns date of first day of week
        ///// </summary>
        ///// <param name="year">week year</param>
        ///// <param name="weekOfYear">week number</param>
        ///// <seealso cref="http://stackoverflow.com/questions/662379/calculate-date-from-week-number"/>
        ///// <returns>date</returns>
        //public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        //{
        //    DateTime jan1 = new DateTime(year, 1, 1);

        //    int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

        //    DateTime firstMonday = jan1.AddDays(daysOffset);

        //    //get first week by ISO
        //    int firstWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.InvariantCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        //    if (firstWeek <= 1)
        //    {
        //        weekOfYear -= 1;
        //    }

        //    return firstMonday.AddDays(weekOfYear * 7);
        //}

        /// <summary>
        /// Calculates ISO week and year number (ISO year can differ from calendar year)
        /// </summary>
        /// <param name="date">date</param>
        /// <param name="year">ISO year</param>
        /// <param name="wk">ISO week</param>
        /// <seealso cref="http://codebetter.com/petervanooijen/2005/09/26/iso-weeknumbers-of-a-date-a-c-implementation/"/>
        public static void GetWeekNumber(DateTime date, out int year, out int wk)
        {
            year = date.Year;
            // Get jan 1st of the year
            DateTime startOfYear = new DateTime(year, 1, 1);
            // Get dec 31st of the year
            DateTime endOfYear = new DateTime(year, 12, 31);
            // ISO 8601 weeks start with Monday 
            // The first week of a year includes the first Thursday 
            // DayOfWeek returns 0 for sunday up to 6 for saterday
            int[] iso8601Correction = { 6, 7, 8, 9, 10, 4, 5 };
            int nds = date.Subtract(startOfYear).Days + iso8601Correction[(int)startOfYear.DayOfWeek];
            wk = nds / 7;
            switch (wk)
            {
                case 0:
                    // Return weeknumber of dec 31st of the previous year
                    GetWeekNumber(startOfYear.AddDays(-1), out year, out wk);
                    break;
                case 53:
                    // If dec 31st falls before thursday it is week 01 of next year
                    if (endOfYear.DayOfWeek < DayOfWeek.Thursday)
                    {
                        wk = 1; year += 1;
                    }
                    break;
                default:
                    break;
            }
        }


        public static int GetLastDayMonth(DateTime date)
        {
            DateTime UltimoDiaMes = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);

            return UltimoDiaMes.Day;
        }




        public static DateTime FirstDateOfWeek(int year, int weekNumber)
        {
            return GetFirstDayOfWeek(year, weekNumber, CultureInfo.CurrentCulture);
        }
        public static DateTime GetFirstDayOfWeek(int year, int weekNumber, System.Globalization.CultureInfo culture)
        {
            System.Globalization.Calendar calendar = culture.Calendar; 
            DateTime firstOfYear = new DateTime(year, 1, 1, calendar); 
            DateTime targetDay = calendar.AddWeeks(firstOfYear, weekNumber); 
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek; 
            while (targetDay.DayOfWeek != firstDayOfWeek) 
            { 
                targetDay = targetDay.AddDays(-1); 
            }
            return targetDay;
        }





    }
}