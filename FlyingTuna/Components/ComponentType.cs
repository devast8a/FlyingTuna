using System;
using System.Collections.Generic;
using System.Reflection;
using FlyingTuna.MPI;
using FlyingTuna.Reflection;
using ML.PCL;

namespace FlyingTuna.Components
{
    public class ComponentType
    {
        public readonly Dictionary<Type, Listener> Listeners = new Dictionary<Type, Listener>();
        public readonly Dictionary<Type, Dependancy> Dependancies = new Dictionary<Type, Dependancy>();
        public readonly Dictionary<Type, Service> Services = new Dictionary<Type, Service>(); 

        public ComponentType(Type type)
        {
            ProcessMethods(type);
            ProcessProperties(type);
        }

        private void ProcessProperties(Type type)
        {
            foreach (var property in type.GetProperties())
            {
                int matches = 0;

                var dAttr = property.GetCustomAttributeOrNull<DependOnAttribute>();

                if (dAttr != null)
                {
                    var dependancy = MetaGetDependancy(property, dAttr);
                    Dependancies.Add(dependancy.Type, dependancy);
                    matches++;
                }

                var sAttr = property.GetCustomAttributeOrNull<UseServiceAttribute>();

                if (sAttr != null)
                {
                    var service = MetaGetService(property, sAttr);
                    Services.Add(service.Type, service);
                    matches++;
                }

                if(matches > 1)
                {
                    throw new Exception("Multiple types");
                }
            }
        }

        private Service MetaGetService(PropertyInfo property, UseServiceAttribute dAttr)
        {
            return new Service(dAttr.Type ?? property.PropertyType, property);
        }

        private Dependancy MetaGetDependancy(PropertyInfo property, DependOnAttribute attribute)
        {
            return new Dependancy(property.PropertyType, property);
        }

        private void ProcessMethods(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                var attribute = method.GetCustomAttributeOrNull<ListenerAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var listener = MetaGetListener(method, attribute);
                Listeners.Add(listener.BindType, listener);
            }
        }

        private Listener MetaGetListener(MethodInfo method, ListenerAttribute attribute)
        {
            var param = method.GetParameters();

            if (param.Length != 2)
            {
                throw new Exception("Bind Error");
            }

            return new Listener(param[1].ParameterType, method);
        }
    }
}