using System;

namespace FlyingTuna.Components
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DependOnAttribute : Attribute
    {
    }
}