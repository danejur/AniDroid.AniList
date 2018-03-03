﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace AniDroid.AniList
{
    public abstract class AniListEnum
    {
        private static readonly Dictionary<Type, Dictionary<string, AniListEnum>> ValueDictionaries = new Dictionary<Type, Dictionary<string, AniListEnum>>();

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
            var type = typeof(T);

            if (!ValueDictionaries.TryGetValue(type, out var dict))
            {
                dict = ValueDictionaries[type] = type.GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.PropertyType == type)
                    .Select(x => x.GetValue(x) as T)
                    .ToDictionary(x => x?.Value, y => y as AniListEnum);
            }

            return dict;
        }

        public static string GetDisplayValue<T>(string value, string defaultValue = "") where T : AniListEnum
        {
            return GetEnum<T>(value)?.DisplayValue ?? defaultValue;
        }

        public static string GetDisplayValue<T>(int index, string defaultValue = "") where T : AniListEnum
        {
            return GetEnumValues<T>().FirstOrDefault(x => x.Index == index)?.DisplayValue ?? defaultValue;
        }

        public static T GetEnum<T>(string value) where T : AniListEnum
        {
            return (GetValueDictionary<T>().TryGetValue(value, out var retEnum) ? retEnum : null) as T;
        }

        public static int GetIndex<T>(string value) where T : AniListEnum
        {
            return GetEnum<T>(value)?.Index ?? -1;

        }

        public static List<T> GetEnumValues<T>() where T : AniListEnum
        {
            var dict = GetValueDictionary<T>();
            return dict.Select(x => x.Value as T).OrderBy(x => x.Index).ToList();
        }

        public bool Equals(AniListEnum obj)
        {
            return obj.GetType() == GetType() && obj.Value == Value;
        }

        public bool Equals(string val)
        {
            return Value == val;
        }
    }
}
