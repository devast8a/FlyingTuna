using System;
using FlyingTuna.Additions.IdenSys;
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
            return new Entity(this, host, entId, new EntityType(){Identifier = typeId});
        }

        public void Register<T>()
        {
        }
    }
}