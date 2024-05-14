

using Business.Models;
using Data.Models;
using Holidays;

namespace Business.Helpers
{
    public static class TollCalculator
    {

        private static int GetTollFee(DateTime date)
        {
            if (IsTollFreeDate(date)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            if ((hour == 6 && 0 <= minute && minute <= 29) ||
                (hour == 8 && 30 <= minute && hour < 15) ||
                (hour == 18 && 0 <= minute  && minute <= 29))
                return 9;
            else if ((hour == 6 && 30 <= minute && minute <= 59) ||
                     (hour == 8 && 0 <= minute  && minute <= 29) ||
                     (hour == 15 && 0 <= minute  && minute <= 29) ||
                     (hour == 17 && 0 <= minute && minute <= 59))
                return 16;
            else if ((hour == 7 && 0 <= minute && minute <= 59) ||
                (hour == 15 && 30 <= minute && minute <= 59) ||
                (hour == 16 && 0 <= minute && minute <= 59))
                return 22;
            else
                return 0;
        }

        private static bool IsTollFreeDate(DateTime date)
        {  
            return ReturnDates.isHoliday(date, ReturnDates.Country.Sweden, true, true) || date.Month == 7;
        }

        public static TollResult CalculateTaxPerDay(List<TollPassage> tollPassages)
        {
            var tolResult = new TollResult
            {
                VehicleRegistrationNumber = tollPassages.FirstOrDefault()!.RegistrationNumber,
                TotalTaxAmount = 0
            };

            int i = 0;
            while (i < tollPassages.Count)
            {
                DateTime currentTimestamp = tollPassages[i].Timestamp;
                int maxHourTax = GetTollFee(currentTimestamp);

                int j = i + 1;
                while (j < tollPassages.Count && tollPassages[j].Timestamp <= currentTimestamp.AddMinutes(60))
                {
                    int tax = GetTollFee(tollPassages[j].Timestamp);
                    if (tax > maxHourTax)
                    {
                        maxHourTax = tax;
                    }
                    j++;
                }

                tolResult.TotalTaxAmount += maxHourTax;
                if (tolResult.TotalTaxAmount > 60)
                    tolResult.TotalTaxAmount = 60;

                i = j;
            }

            return tolResult;
        }
    }
}
