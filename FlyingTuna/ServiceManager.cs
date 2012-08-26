using System;
using System.Collections.Generic;

namespace FlyingTuna
{
    public class ServiceManager
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public T GetProvider<T>()
        {
            return (T)GetProvider(typeof(T));
        }

        public object GetProvider(Type type)
        {
            return _services[type];
        }

        public void SetProvider<T>(T service)
        {
            _services.Add(typeof(T), service);
        }
    }
}