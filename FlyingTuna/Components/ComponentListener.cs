using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyingTuna.MPI;

namespace FlyingTuna.Components
{
    public class ComponentListener
    {
        public readonly Component Component;
        public readonly List<Listener> Listeners = new List<Listener>();

        public ComponentListener(Component component)
        {
            Component = component;
        }

        public void Add(Listener listener)
        {
            Listeners.Add(listener);
        }

        public void Invoke(IMessageSender sender, Message message)
        {
            foreach(var listener in Listeners)
            {
                listener.Invoke(Component, sender, message);
            }
        }
    }
}
