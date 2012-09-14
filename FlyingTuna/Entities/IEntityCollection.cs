using System;
using System.Collections.Generic;

namespace FlyingTuna.Entities
{
    public interface IEntityCollection
    {
        EntityType Resolve(int typeid);
        void AddEntity(Entity createObject);
        void RemoveEntity(Entity ent);

        event Action<Entity> OnAddEntity;

        IEnumerable<Entity> GetList();
        Entity GetEntity(int id);
    }
}