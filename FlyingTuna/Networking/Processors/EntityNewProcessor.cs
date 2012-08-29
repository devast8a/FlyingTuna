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
            _collection.AddEntity(_factory.CreateObject<Entity>(new ID{IdentifierNumber = reader.ReadInt32()}));
        }
    }
}
