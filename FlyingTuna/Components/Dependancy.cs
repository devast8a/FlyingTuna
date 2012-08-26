using System;
using System.Reflection;
using FlyingTuna.Components;

namespace ML.PCL
{
    public class Dependancy
    {
        public Dependancy(Type type, PropertyInfo property)
        {
            Type=type;
            _property=property;
        }

        private readonly PropertyInfo _property;
        public Type Type{ get; private set;}

        public void Set(Component component, Component target)
        {
            _property.SetValue(component, target, null);
        }
    }
}