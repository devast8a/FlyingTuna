﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FlyingTuna.MPI
{
    class ListenerMultiDebug<T> : Listener where T : Message
    {
        public ListenerMultiDebug(Type bindType, MethodInfo methodInfo) : base(bindType, methodInfo)
        {
        }

        public override void Invoke(object target, IMessageSender sender, Message message)
        {
            if (!message.NoExtraLogging)
            {
                Console.WriteLine("{0} ==> {1}::{2}", message.GetType().Name, MethodInfo.DeclaringType.FullName, MethodInfo.Name);
            }

            var d = (Action<IMessageSender, T>)Delegate.CreateDelegate(typeof(Action<IMessageSender, T>), target, MethodInfo);
            d(sender, (T)message);
        }
    }
}
