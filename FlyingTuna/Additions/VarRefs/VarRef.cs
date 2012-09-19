using System;

namespace FlyingTuna.Additions.VarRefs
{
    public class VarRef<T> : VariableReference
    {
        protected T Variable;
        
        public virtual bool ShallowCopy
        {
            get { return false; }
        }

        public VarRef(string name) : base(name, typeof(T))
        {
        }

        public VarRef(string name, T value) : base(name, typeof(T))
        {
            Variable = value;
        }

        public T Value
        {
            get
            {
                return Variable;
            }
        }

        public override object GetVariable()
        {
            return Variable;
        }

        public override void SetVariable(object variable, IVariableContainer container)
        {
            Variable = (T)variable;
        }

        public override VariableReference GetDirectCopy(IVariableContainer container)
        {
            return this;
        }

        public override VariableReference GetReferenceCopy(IVariableContainer container)
        {
            var v = new ShallowVarRef<T>(Name, Variable);
            container.Overwrite(v);
            return v;
        }

        public virtual void Set(IVariableContainer container, T value)
        {
            Variable = value;
        }
    }
}