using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingTuna.Components
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UseServiceAttribute : Attribute
    {
        public readonly Type Type;

        public UseServiceAttribute()
        {
            Type = null;
        }

        public UseServiceAttribute(Type type)
        {
            Type=type;
        }
    }
}
