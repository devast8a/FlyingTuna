using System;
using System.Collections.Generic;
using System.Reflection;
using FlyingTuna.Additions.VarRefs;
using FlyingTuna.MPI;
using FlyingTuna.Reflection;

namespace FlyingTuna.Components
{
    public class ComponentType
    {
        public readonly Dictionary<Type, Listener> Listeners = new Dictionary<Type, Listener>();
        public readonly Dictionary<Type, Dependancy> Dependancies = new Dictionary<Type, Dependancy>();
        public readonly Dictionary<Type, Service> Services = new Dictionary<Type, Service>(); 
        public readonly Dictionary<string, VariableReferenceC> Variables = new Dictionary<string, VariableReferenceC>();

        public ComponentType(Type type)
        {
            //DEBUG
            if(!typeof(Component).IsAssignableFrom(type))
            {
                throw new Exception("Invalid component type");
            }

            ProcessMethods(type);
            ProcessProperties(type);
            ProcessFields(type);
        }

        private static readonly Dictionary<Type, ComponentType> Cache = new Dictionary<Type, ComponentType>();
        public static ComponentType Get(Type type)
        {
            ComponentType value;

            if (!Cache.TryGetValue(type, out value))
            {
                value = new ComponentType(type);
                Cache.Add(type, value);
            }

            return value;
        }

        private void ProcessFields(Type type)
        {
            foreach(var field in type.GetFields())
            {
                var ftype = field.FieldType;

                if(ftype.IsGenericType &&
                    ftype.GetGenericTypeDefinition() == typeof(VarRef<>))
                {
                    Variables.Add(field.Name, new VariableReferenceC(){Field = field, Name = field.Name, Type = ftype.GetGenericArguments()[0]});
                }
            }
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

            if (param.Length == 1)
            {
                var type = param[0].ParameterType;
                if(typeof(Message).IsAssignableFrom(type))
                {
                    var m = typeof(ListenerMessageDebug<>).MakeGenericType(type);

                    return (Listener)Activator.CreateInstance(m, type, method);
                    //return new ListenerMessage(type, method);
                }

                throw new Exception();
            }

            if(param.Length == 2)
            {
                if(param[0].ParameterType != typeof(IMessageSender))
                {
                    throw new Exception();
                }

                var type = param[1].ParameterType;

                if (typeof(Message).IsAssignableFrom(type))
                {
                    var m = typeof(ListenerMultiDebug<>).MakeGenericType(type);

                    return (Listener)Activator.CreateInstance(m, type, method);
                }

                throw new Exception();
            }

            throw new Exception();
        }
    }
}