using System.Globalization;

namespace Drim.Interfaces.MVC.Helpers
{
    public static class DateTimeStrExtensions
    {
        public static string ToStringMonth(this int Month)
        {
            switch (Month)
            {
                case 1: return "Enero";
                case 2:  return "Febrero";
                case 3: return "Marzo";
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Septiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                case 12: return "Diciembre";
                default: return "No Found";
            }
        }


       public static DateTime FirstDateOfWeek(this DateTime jan1, int year, int weekOfYear)
        {
             jan1 = new DateTime(year, 1, 1);

            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                weekOfYear -= 1;
            }

            return firstMonday.AddDays(weekOfYear * 7);
        }

        public static string ToStringFormat24h(this TimeSpan hora) 
        {
            string horaSrt = string.Empty;
            horaSrt = string.Format("{0}:{1}", (hora.Hours<10? "0":"") +  hora.Hours,(hora.Minutes<10? "0":"") + hora.Minutes);
            return horaSrt;
        }

        public static string ToStringFormat24h(this DateTime hora)
        {
            string horaSrt = string.Empty;
            horaSrt = string.Format("{0}:{1}", (hora.Hour < 10 ? "0" : "") + hora.Hour, (hora.Minute < 10 ? "0" : "") + hora.Minute);
            return horaSrt;
        }
    }
}
