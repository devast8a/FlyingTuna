using System;
using System.IO;
using FlyingTuna.Entities;
using FlyingTuna.Factories;
using FlyingTuna.MPI;

namespace FlyingTuna.Networking.Processors
{
    public class EntityMessageProcessor : IPacketProcessor
    {
        private readonly FactoryManager _factory;
        private readonly IEntityCollection _collection;

        public EntityMessageProcessor(IHost host, IEntityCollection collection)
        {
            _factory = host.FactoryManager;
            _collection=collection;
        }

        // Create the object
        public void Process(Connection c, byte[] buffer, short pos, short len)
        {
            var ms = new MemoryStream(buffer, pos, len, false);
            var reader = new BinaryReader(ms);

            // Ok first thing
            int payload = len - 8;

            int entityId = reader.ReadInt32();
            int messageId = reader.ReadInt32();

            
            var ent = _collection.GetEntity(entityId);

            Console.WriteLine("Received message from remote side.");

            // We need to deserialize the message
            var message = _factory.CreateObject<Message>(messageId);


            // Send the message
            ent.SendMessage(message);
        }
    }
}
