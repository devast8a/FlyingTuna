using System;
using System.Collections.Generic;
using FlyingTuna.Entities;
using ML.PCL;

namespace FlyingTuna.Components
{
    public class Component
    {
        // Make this immutable
        public Entity ComponentParent;

        public readonly ComponentType Type;
        private readonly Dictionary<Type, ComponentType> _cache = new Dictionary<Type, ComponentType>(); 

        public Component()
        {
            if(!_cache.TryGetValue(GetType(), out Type))
            {
                Type = new ComponentType(GetType());
                _cache.Add(GetType(), Type);
            }
        }

        public virtual void OnInitialize(){}
    }
}