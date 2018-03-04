using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AniDroid.AniList.DataTypes
{
    public class FuzzyDate
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }

        public DateTime? GetDate()
        {
            if (Year.HasValue && Month.HasValue && Day.HasValue)
            {
                return new DateTime(Year.Value, Month.Value, Day.Value);
            }

            return null;
        }

        public string GetFuzzyDateString()
        {
            if (!Month.HasValue)
            {
                return Year?.ToString();
            }

            var retString = DateTimeFormatInfo.CurrentInfo.GetMonthName(Month.Value);
            retString += Day.HasValue ? $" {Day.Value}" : "";
            retString += Year.HasValue ? $", {Year.Value}" : "";
            return retString;
        }
    }
}
