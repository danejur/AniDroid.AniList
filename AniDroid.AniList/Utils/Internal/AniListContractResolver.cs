using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AniDroid.AniList.Utils.Internal
{
    internal class AniListContractResolver : DefaultContractResolver
    {
        private static AniListContractResolver _instance;

        private readonly Dictionary<Type, JsonConverter> _converterCache;

        // Manual Singleton ftw!
        public static AniListContractResolver Instance
            => _instance ?? (_instance = new AniListContractResolver());

        public readonly Dictionary<Type, Type> InterfaceConcreteMap;

        private AniListContractResolver()
        {
            _converterCache = new Dictionary<Type, JsonConverter>();

            NamingStrategy = new CamelCaseNamingStrategy();
            InterfaceConcreteMap = new Dictionary<Type, Type>
            {
                { typeof(IList<>), typeof(List<>) },
                { typeof(ICollection<>), typeof(List<>) },
            };
        }

        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (!objectType.IsInterface)
            {
                return base.ResolveContractConverter(objectType);
            }

            var isGeneric = objectType.IsGenericType;
            var interfaceType = isGeneric
                ? objectType.GetGenericTypeDefinition()
                : objectType;

            if (!InterfaceConcreteMap.ContainsKey(interfaceType))
            {
                return base.ResolveContractConverter(objectType);
            }

            var actualType = InterfaceConcreteMap[interfaceType];
            var concreteGenericType = actualType.MakeGenericType(isGeneric ? objectType.GetGenericArguments() : new Type[0]);

            if (_converterCache.ContainsKey(concreteGenericType))
            {
                return _converterCache[concreteGenericType];
            }

            var converterType = typeof(AniListJsonConverter<>).MakeGenericType(concreteGenericType);
            return _converterCache[concreteGenericType] = Activator.CreateInstance(converterType) as JsonConverter;
        }
    }
}
