using System;
using System.Reflection;

namespace FlyingTuna.MPI
{
    public abstract class Listener
    {
        protected Listener(Type bindType, MethodInfo methodInfo)
        {
            MethodInfo=methodInfo;
            BindType=bindType;
        }

        public readonly MethodInfo MethodInfo;
        public readonly Type BindType;

        public abstract void Invoke(object target, IMessageSender sender, Message message);
    }
}