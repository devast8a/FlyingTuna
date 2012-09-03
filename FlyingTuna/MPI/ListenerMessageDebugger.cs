using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FlyingTuna.MPI
{
    class ListenerMessageDebugger<T> : Listener where T : Message
    {
        public ListenerMessageDebugger(Type bindType, MethodInfo methodInfo) : base(bindType, methodInfo)
        {
        }

        public override void Invoke(object target, IMessageSender sender, Message message)
        {
            var d = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), target, MethodInfo);
            d((T)message);
        }
    }
}
