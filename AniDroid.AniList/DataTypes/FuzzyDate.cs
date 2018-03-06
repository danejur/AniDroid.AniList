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

        public bool IsValid()
        {
            return Year.HasValue || Month.HasValue;
        }

        public bool Equals(FuzzyDate date)
        {
            var equal = true;

            if (Year.HasValue && date.Year.HasValue)
            {
                equal = Year.Value == date.Year.Value;
            }

            if (Month.HasValue && date.Month.HasValue)
            {
                equal = Month.Value == date.Month.Value;
            }

            if (Day.HasValue && date.Day.HasValue)
            {
                equal = Day.Value == date.Day.Value;
            }

            return equal;
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
