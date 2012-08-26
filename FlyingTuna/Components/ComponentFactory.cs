using System;
using FlyingTuna.Entities;
using ML.PCL;

namespace FlyingTuna.Components
{
    public class ComponentFactory
    {
        public Component GetComponent(Type type)
        {
            return (Component) Activator.CreateInstance(type);
        }

        public Entity NewEntity(IHost host, ID typeId, ID entId)
        {
            return new Entity(this, host, typeId, new EntityType(){Identifier = entId});
        }

        public void Register<T>()
        {
        }
    }
}