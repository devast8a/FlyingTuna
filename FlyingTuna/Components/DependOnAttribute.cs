using System;

namespace FlyingTuna.Components
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependOnAttribute : Attribute
    {
        public Type Component;

        public DependOnAttribute(Type type)
        {
            if(typeof(Component).IsAssignableFrom(type))
            {
                Component = type;
            }
        }
    }
}