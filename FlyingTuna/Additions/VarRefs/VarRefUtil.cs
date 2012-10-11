using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlyingTuna.Additions.VarRefs
{
    // TODO: Choose a better class name
    public static class VarRefUtil
    {
        static public VariableReference Create(string name, Type type)
        {
            var refType = typeof(VarRef<>).MakeGenericType(type);
            var e = (VariableReference)Activator.CreateInstance(refType, name);

            return e;
        }

        static public VariableReference CreateShallowRef(string name, Type type, object value)
        {
            var refType = typeof(VarRef<>).MakeGenericType(type);
            var e = (VariableReference)Activator.CreateInstance(refType, name, value);

            return e;
        }

        public static void ImportInto(IVariableContainer source, IVariableContainer target)
        {
            foreach (var variable in source.GetVariables())
            {
                target.Overwrite(variable.GetReferenceCopy(source));
            }
        }
    }
}
