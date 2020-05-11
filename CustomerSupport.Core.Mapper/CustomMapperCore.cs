using System;
using System.Collections.Generic;
using System.Linq;


namespace CustomerSupport.Core.Mapper
{
    public abstract class CustomMapperCore
    {
        private readonly Dictionary<KeyValuePair<Type, Type>, Delegate> mappingConfig = new Dictionary<KeyValuePair<Type, Type>, Delegate>();
        public  TDestination MapOne<TDestination>(object sourceItem)
        {
            if (sourceItem != null)
            {
                Type sourceType = sourceItem.GetType();
                var key = new KeyValuePair<Type, Type>(sourceType, typeof(TDestination));
                if (mappingConfig.ContainsKey(key))
                    return (TDestination)mappingConfig[key].Method.Invoke(this, new object[] { sourceItem });
                else
                    throw new Exception($"Mapping between {sourceType} and {typeof(TDestination)} is not registered");
            }
            else
                throw new NullReferenceException();
        }

        public IEnumerable<TDestination> MapMany<TDestination>(IEnumerable<object> sourceCollection)
        {
            if (sourceCollection == null)
                throw new NullReferenceException();
            if (sourceCollection.Count() != 0)
            {
                Type sourceType = sourceCollection.First().GetType();
                var key = new KeyValuePair<Type, Type>(sourceType, typeof(TDestination));
                if (mappingConfig.ContainsKey(key) == false)
                    throw new Exception($"Mapping between {sourceType} and {typeof(TDestination)} is not registered");
            }
            List<TDestination> mappedCollection = new List<TDestination>();
            foreach (object item in sourceCollection)
                mappedCollection.Add(MapOne<TDestination>(item));
            return mappedCollection;
           // return sourceCollection.Select(item => MapOne<TDestination>(item));
        }

        public void RegisterMapping<TSource, TDestination>(Func<TSource, TDestination> mappingFunction)
        {
            mappingConfig.Add(new KeyValuePair<Type, Type>(typeof(TSource), typeof(TDestination)),
                              mappingFunction);
        }

    }
}
