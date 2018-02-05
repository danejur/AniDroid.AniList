using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AniDroid.AniList.Internal
{
    public abstract class AniListEnum
    {
        private AniListEnum() { }

        protected AniListEnum(string val, string displayVal) { Value = val; DisplayValue = displayVal; }

        public string Value { get; }
        public string DisplayValue { get; }

        public static Dictionary<string, string> GetValueDictionary<T>() where T : AniListEnum
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.PropertyType == typeof(T))
                .Select(x => x.GetValue(x) as T)
                .ToDictionary(x => x.Value, y => y.DisplayValue);
        }
    }
}
