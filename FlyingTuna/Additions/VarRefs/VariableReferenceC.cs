using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FlyingTuna.Components;

namespace FlyingTuna.Additions.VarRefs
{
    public class VariableReferenceC
    {
        public FieldInfo Field;
        public Type Type;
        public string Name;

        public void Set(Component component, VariableReference varRef)
        {
            Field.SetValue(component, varRef);
        }
    }
}
