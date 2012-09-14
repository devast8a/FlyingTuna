using System;
using System.Reflection;

namespace FlyingTuna.Components
{
    public class Dependancy
    {
        public Dependancy(Type type, FieldInfo field)
        {
            Type=type;
            _field=field;
        }

        private readonly FieldInfo _field;
        public Type Type{ get; private set;}

        public void Set(Component component, Component target)
        {
            _field.SetValue(component, target);
        }
    }
}