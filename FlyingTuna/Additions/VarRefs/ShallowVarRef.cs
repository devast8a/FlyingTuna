using System;

namespace FlyingTuna.Additions.VarRefs
{
    public class ShallowVarRef<T> : VarRef<T>
    {
        public ShallowVarRef(string name, object value) : base(name)
        {
            Console.WriteLine("^ Shallow");
            Variable = (T)value;
        }

        public override VariableReference GetDirectCopy(IVariableContainer container)
        {
            var newRef = new VarRef<T>(Name, Variable);
            container.Overwrite(newRef);
            return newRef;
        }

        public override VariableReference GetReferenceCopy(IVariableContainer container)
        {
            return this;
        }

        public override bool ShallowCopy
        {
            get { return true; }
        }

        public override void SetVariable(object variable, IVariableContainer container)
        {
            GetDirectCopy(container).SetVariable(variable, container);
        }

        public override void Set(IVariableContainer container, T value)
        {
            SetVariable(value, container);
        }
    }
}