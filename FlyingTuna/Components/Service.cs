using System;
using System.Reflection;

namespace FlyingTuna.Components
{
    public class Service
    {
        public readonly Type Type;
        public readonly FieldInfo FieldInfo;

        public Service(Type type, FieldInfo field)
        {
            Type = type;
            FieldInfo = field;
        }

        public void Set(Component component, object service)
        {
            FieldInfo.SetValue(component, service);
        }
    }
}