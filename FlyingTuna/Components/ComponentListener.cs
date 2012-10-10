using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyingTuna.MPI;

namespace FlyingTuna.Components
{
    public class ComponentListener
    {
        public readonly Dictionary<Component, Listener> Listeners = new Dictionary<Component, Listener>();

        public ComponentListener()
        {
        }

        public void Add(Component component, Listener listener)
        {
            Listeners.Add(component, listener);
        }

        public void Invoke(IMessageSender sender, Message message)
        {
            foreach(var listener in Listeners)
            {
                listener.Value.Invoke(listener.Key, sender, message);
            }
        }
    }
}
