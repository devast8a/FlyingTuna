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
        public readonly Listener Listener;

        public ComponentListener(Component component, Listener listener)
        {
            Component=component;
            Listener=listener;
        }
    }
}
