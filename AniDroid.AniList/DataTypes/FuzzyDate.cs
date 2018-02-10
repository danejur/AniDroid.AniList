using System;
using System.Collections.Generic;
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
    }
}
