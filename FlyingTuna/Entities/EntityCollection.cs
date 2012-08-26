using System;
using System.Collections.Generic;

namespace FlyingTuna.Entities
{
    public class EntityCollection : IEntityCollection
    {
        public Dictionary<int, Entity> Entities = new Dictionary<int, Entity>();
        public List<EntityType> Types = new List<EntityType>();

        public EntityType Resolve(int typeid)
        {
            return Types[typeid];
        }

        public void AddEntity(Entity createObject)
        {
            if(OnAddEntity != null)
            {
                OnAddEntity(createObject);
            }

            Entities.Add(createObject.Identifier.IdentifierNumber, createObject);
        }

        public event Action<Entity> OnAddEntity;
        public IEnumerable<Entity> GetList()
        {
            foreach(var ent in Entities)
            {
                yield return ent.Value;
            }
        }

        public Entity GetEntity(int id)
        {
            return Entities[id];
        }
    }
}