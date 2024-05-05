

using Holidays;

namespace Business.Helpers
{
    public static class TollCalculator
    {

        public static int GetTollFee(DateTime date)
        {
            if (IsTollFreeDate(date)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            if ((hour == 6 && minute >= 0 && minute < 30) ||
                (hour == 8 && minute >= 0 && minute < 30) ||
                (hour == 15 && minute >= 0 && minute < 30) ||
                (hour == 18 && minute >= 0 && minute < 30))
            {
                return 9;
            }
            else if ((hour == 6 && minute >= 30) ||
                     (hour >= 7 && hour < 8) ||
                     (hour == 15 && minute >= 30) ||
                     (hour >= 16 && hour < 17))
            {
                return 16;
            }
            else if ((hour == 7 && minute >= 0) ||
                     (hour >= 8 && hour < 15) ||
                     (hour == 17 && minute >= 0) ||
                     (hour >= 18 && hour < 19))
            {
                return 22;
            }
            else
            {
                return 0;
            }
        }

        private static bool IsTollFreeDate(DateTime date)
        {
            
            return ReturnDates.isHoliday(date, ReturnDates.Country.Sweden, true, true) || date.Month == 7;
        }
    }
}
