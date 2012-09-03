using System;
using System.IO;
using FlyingTuna.Additions.IdenSys;
using FlyingTuna.Entities;
using FlyingTuna.Factories;

namespace FlyingTuna.Networking.Processors
{
    public class EntityNewProcessor : IPacketProcessor
    {
        private readonly IEntityCollection _collection;
        private readonly FactoryManager _factory;

        public EntityNewProcessor(IHost host, IEntityCollection collection)
        {
            _factory = host.FactoryManager;
            _collection=collection;
        }

        // Create the object
        public void Process(Connection c, byte[] buffer, short pos, short len)
        {
            var reader = new BinaryReader(new MemoryStream(buffer, pos, len, false));

            var objId = new ID(){IdentifierNumber = reader.ReadInt32() };
            var id = reader.ReadInt32();
            var ent = _factory.CreateObject<Entity>(objId);

            ent.Identifier.IdentifierNumber = id;

            _collection.AddEntity(ent);
        }
    }
}
