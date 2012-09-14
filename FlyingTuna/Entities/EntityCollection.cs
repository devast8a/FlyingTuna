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

        int _highestID = 10000;

        public void AddEntity(Entity createObject)
        {
            if(OnAddEntity != null)
            {
                OnAddEntity(createObject);
            }

            if (createObject.Identifier.IdentifierNumber == 0)
            {
                createObject.Identifier.IdentifierNumber = _highestID++;
            }else if(createObject.Identifier.IdentifierNumber > _highestID){
                _highestID = createObject.Identifier.IdentifierNumber;
            }

            Entities.Add(createObject.Identifier.IdentifierNumber, createObject);
        }

        public void RemoveEntity(Entity ent)
        {
            Entities.Remove(ent.Identifier.IdentifierNumber);
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