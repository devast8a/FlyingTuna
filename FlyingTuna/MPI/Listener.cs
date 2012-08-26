using System;
using System.Reflection;

namespace FlyingTuna.MPI
{
    public struct Listener
    {
        public Listener(Type bindType, MethodInfo methodInfo)
        {
            MethodInfo=methodInfo;
            BindType=bindType;
        }

        public readonly MethodInfo MethodInfo;
        public readonly Type BindType;
    }
}