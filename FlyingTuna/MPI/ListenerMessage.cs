﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FlyingTuna.MPI
{
    public class ListenerMessage : Listener
    {
        public ListenerMessage(Type bindType, MethodInfo methodInfo) : base(bindType, methodInfo)
        {
        }

        public override void Invoke(object target, IMessageSender sender, Message message)
        {
            MethodInfo.Invoke(target, new object[] { message });
        }
    }
}
