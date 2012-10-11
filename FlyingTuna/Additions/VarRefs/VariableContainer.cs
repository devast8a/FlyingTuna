using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingTuna.Additions.VarRefs
{
    //TODO try to allow variable containers to use variable references automatically
    public class VariableContainer : IVariableContainer
    {
        public VariableContainer(){}

        public VariableContainer(VariableContainer parent)
        {
            VarRefUtil.ImportInto(parent, this);
        }

        private readonly Dictionary<string, VariableReference> _variables = new Dictionary<string, VariableReference>(); 
 
        // Return a variable reference for the variable requested, create one if it doesn't exist.
        public VariableReference GetOrCreate(string key, Type type)
        {
            VariableReference value;
            if(!_variables.TryGetValue(key, out value))
            {
                value = VarRefUtil.Create(key, type);
                _variables.Add(key, value);
            }else
            {
                // Check type
                if(value.Type != type)
                {
                    throw new Exception(string.Format("Variable Name= {2}\nRequested type= {0}\nActual Type= {1}", value.Type, type, key));
                }
            }
            
            return value;
        }

        public VarRef<T> GetOrCreate<T>(string name)
        {
            return (VarRef<T>)GetOrCreate(name, typeof(T));
        }

        public VariableReference Get(string key, Type type)
        {
            VariableReference value;
            if (!_variables.TryGetValue(key, out value))
            {
                throw new KeyNotFoundException(key);
            }

            // Check type
            if (value.Type != type)
            {
                throw new Exception("Yeah this isn't the type you requested.");
            }

            return value;
        }

        public T Get<T>(string key)
        {
            return ((VarRef<T>)Get(key, typeof(T))).Value;
        } 

        public void Set<T>(string name, T value)
        {
            GetOrCreate<T>(name).Set(this, value);
        }

        public void Overwrite(VariableReference varRef)
        {
            _variables[varRef.Name] = varRef;
        }

        public IEnumerable<VariableReference> GetVariables()
        {
            return _variables.Select(v => v.Value);
        }
    }
}
