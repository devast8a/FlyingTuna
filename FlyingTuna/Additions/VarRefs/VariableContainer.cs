using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingTuna.Additions.VarRefs
{
    public class VariableContainer
    {
        private readonly Dictionary<string, VariableReference> _variables = new Dictionary<string, VariableReference>(); 
 
        public VariableReference GetOrCreate(string key, Type type)
        {
            VariableReference value;
            if(!_variables.TryGetValue(key, out value))
            {
                value = Create(key, type);
                _variables.Add(key, value);
            }else
            {
                // Check type
                if(value.Type != type)
                {
                    throw new Exception("Yeah this isn't the type you requested.");
                }
            }
            
            return value;
        }

        private VariableReference Create(string name, Type type)
        {
            var refType = typeof(VarRef<>).MakeGenericType(type);
            var e = (VariableReference)Activator.CreateInstance(refType, name, type);

            return e;
        }

        public VarRef<T> GetOrCreate<T>(string name)
        {
            return (VarRef<T>)GetOrCreate(name, typeof(T));
        } 

        public void Set<T>(string name, T value)
        {
            GetOrCreate<T>(name).Write(value);
        }
    }
}
