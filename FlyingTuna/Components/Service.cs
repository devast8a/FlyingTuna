using System;
using System.Reflection;

namespace FlyingTuna.Components
{
    public class Service
    {
        public readonly Type Type;
        public readonly PropertyInfo PropertyInfo;

        public Service(Type type, PropertyInfo propertyInfo)
        {
            Type = type;
            PropertyInfo=propertyInfo;
        }

        public void Set(Component component, object service)
        {
            PropertyInfo.SetValue(component, service, null);
        }
    }
}