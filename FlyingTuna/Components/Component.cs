using System;
using System.Collections.Generic;
using FlyingTuna.Entities;

namespace FlyingTuna.Components
{
    public class Component
    {
        // Make this immutable
        public Entity ComponentParent;

        public readonly ComponentType Type;
        public Component()
        {
            Type = ComponentType.Get(GetType());
        }

        public virtual void OnInitialize(){}
    }
}