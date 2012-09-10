using System;
using System.Collections.Generic;
using FlyingTuna.Additions.VarRefs;
using FlyingTuna.Entities;

namespace FlyingTuna.Components
{
    public class Component : IVariableContainer
    {
        // Make this immutable
        public Entity ComponentParent;

        public readonly ComponentType Type;
        public Component()
        {
            Type = ComponentType.Get(GetType());
        }

        public virtual void OnInitialize(){}
        
        public void Overwrite(VariableReference varRef)
        {
            VariableReferenceC value;
              
            if(Type.Variables.TryGetValue(varRef.Name, out value))
            {
                value.Set(this, varRef);
            }
        }
    }
}