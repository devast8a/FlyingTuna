using System;

namespace FlyingTuna.Additions.VarRefs
{
    public abstract class VariableReference
    {
        public abstract object GetVariable();
        public abstract void SetVariable(object variable, IVariableContainer container);

        public abstract VariableReference GetDirectCopy(IVariableContainer container);
        public abstract VariableReference GetReferenceCopy(IVariableContainer container);

        public string Name;
        public Type Type;

        protected VariableReference(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}