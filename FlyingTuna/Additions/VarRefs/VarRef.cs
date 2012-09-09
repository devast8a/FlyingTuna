using System;

namespace FlyingTuna.Additions.VarRefs
{
    public class VarRef<T> : VariableReference
    {
        private T _variable;

        public VarRef(string name, Type type) : base(name, type)
        {
        }

        public T Value
        {
            get
            {
                return _variable;
            }
        }

        public override object GetVariable()
        {
            return _variable;
        }

        public void Write(T value)
        {
            _variable = value;
        }
    }
}