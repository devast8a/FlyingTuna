using System;
using System.Collections.Generic;
using FlyingTuna.Additions.VarRefs;
using FlyingTuna.Entities;

namespace FlyingTuna.Components
{
    public class Component : IVariableContainer
    {
        // Make this immutable
        public Entity Entity;

        public readonly ComponentType Type;
        public Component()
        {
            Type = ComponentType.Get(GetType());
        }

        public virtual void OnInitialize(IHost host){}
        
        public void Overwrite(VariableReference varRef)
        {
            Entity.Overwrite(varRef);
        }

        public IEnumerable<VariableReference> GetVariables()
        {
            throw new NotImplementedException();
        }

        public void OverwriteLocal(VariableReference varRef)
        {
            VariableReferenceC value;

            if (Type.Variables.TryGetValue(varRef.Name, out value))
            {
                value.Set(this, varRef);
            }
        }
    }
}