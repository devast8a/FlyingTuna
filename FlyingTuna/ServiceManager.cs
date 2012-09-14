using System;
using System.Collections.Generic;

namespace FlyingTuna
{
    public class ServiceManager
    {
        private readonly Dictionary<string, object> _services = new Dictionary<string, object>();
        
        public T GetProvider<T>()
        {
            return (T)GetProvider(typeof(T).ToString());
        }

        public T GetProvider<T>(string name)
        {
            return (T)GetProvider(name);
        }

        public object GetProvider(Type type)
        {
            return GetProvider(type.ToString());
        }

        private object GetProvider(string name)
        {
            return _services[name];
        }

        public void SetProvider<T>(T service)
        {
            SetProvider(typeof(T).ToString(), service);
        }

        public void SetProvider(string name, object service)
        {
            _services.Add(name, service);
        }
    }
}