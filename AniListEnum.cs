using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AniDroid.AniList
{
    public abstract class AniListEnum
    {
        private static Dictionary<Type, Dictionary<string, AniListEnum>> ValueDictionaries = new Dictionary<Type, Dictionary<string, AniListEnum>>();

        private AniListEnum() { }

        protected AniListEnum(string val, string displayVal, int index)
        {
            Value = val;
            DisplayValue = displayVal;
            Index = index;
        }

        public string Value { get; }
        public string DisplayValue { get; }
        public int Index { get; }

        private static Dictionary<string, AniListEnum> GetValueDictionary<T>() where T : AniListEnum
        {
            if (ValueDictionaries.ContainsKey(typeof(T)))
            {
                return ValueDictionaries[typeof(T)];
            }

            var dict = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.PropertyType == typeof(T))
                .Select(x => x.GetValue(x) as T)
                .ToDictionary(x => x.Value, y => y as AniListEnum);

            ValueDictionaries.Add(typeof(T), dict);

            return dict;
        }

        public static string GetDisplayValue<T>(string value, string defaultValue = "") where T : AniListEnum
        {
            var dict = GetValueDictionary<T>();

            if (dict.ContainsKey(value))
            {
                return dict[value].DisplayValue;
            }

            return defaultValue;
        }

        public static string GetDisplayValue<T>(int index, string defaultValue = "") where T : AniListEnum
        {
            return GetEnumValues<T>().FirstOrDefault(x => x.Index == index)?.DisplayValue ?? defaultValue;
        }

        public static T GetEnum<T>(string value) where T : AniListEnum
        {
            return GetEnumValues<T>().FirstOrDefault(x => x.Value == value);
        }

        public static int GetIndex<T>(string value) where T : AniListEnum
        {
            var dict = GetValueDictionary<T>();

            if (dict.ContainsKey(value))
            {
                return dict[value].Index;
            }

            return -1;
        }

        public static List<T> GetEnumValues<T>() where T : AniListEnum
        {
            var dict = GetValueDictionary<T>();
            return dict.Select(x => x.Value as T).OrderBy(x => x.Index).ToList();
        }
    }
}
