using System;
using System.Collections.Generic;
using FlyingTuna.Additions.IdenSys;

namespace FlyingTuna.Factories
{
    public class FactoryManager
    {
        public FactoryManager(IHost host)
        {
            _host=host;
        }

        readonly List<IFactory> _factories = new List<IFactory>();
        private readonly IHost _host;

        public void AddFactory(IFactory factory)
        {
            _factories.Add(factory);
        }
        
        public T CreateObject<T>(ID id)
        {
            Console.WriteLine("[{0}] Factory Requested {1}", _host, id.IdentifierNumber);
            var o = _factories[id.IdentifierNumber].CreateObject();

            if(o is T)
            {
                return (T)o;
            }

            throw new Exception("Unexpected type");
        }
    }
}
