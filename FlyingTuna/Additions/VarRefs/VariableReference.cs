using System;

namespace FlyingTuna.Additions.VarRefs
{
    public abstract class VariableReference
    {
        public abstract object GetVariable();

        public string Name;
        public Type Type;

        protected VariableReference(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}