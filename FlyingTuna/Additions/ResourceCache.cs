using System;
using System.Collections.Generic;

namespace FlyingTuna.Additions
{
    public class ResourceCache
    {
        class Inner
        {
            public readonly Dictionary<string, object> Values = new Dictionary<string, object>();
            public Func<string,object> Loader;
        }

        private readonly Dictionary<Type, Inner> _resource = new Dictionary<Type,Inner>();

        private Inner GetResourceCache(Type type)
        {
            Inner value;
            if (!_resource.TryGetValue(type, out value))
            {
                value = new Inner();
                _resource.Add(type, value);
            }

            return value;
        }

        public Func<string, object> GetLoader<T>()
        {
            return GetResourceCache(typeof(T)).Loader;
        }

        public void SetLoader<T>(Func<string, object> value)
        {
            GetResourceCache(typeof(T)).Loader = value;
        }

        public T Load<T>(string name)
        {
            var rescache = GetResourceCache(typeof(T));

            object value;

            if (!rescache.Values.TryGetValue(name, out value))
            {
                if (rescache.Loader == null)
                {
                    throw new Exception("Called load before setting loader");
                }

                value = rescache.Loader(name);

                if(!(value is T))
                {
                    throw new Exception("Loaded resource is not of type " + typeof(T));
                }

                rescache.Values.Add(name, value);
            }

            return (T)value;
        }

        public void Store<T>(string name, T obj, bool overwrite = false)
        {
            var rescache = GetResourceCache(typeof(T));

            if(overwrite)
            {
                if(rescache.Values.ContainsKey(name))
                {
                    throw new Exception("Already contains a resource with name " + name);
                }
            }

            rescache.Values[name] = obj;
        }
    }
}
