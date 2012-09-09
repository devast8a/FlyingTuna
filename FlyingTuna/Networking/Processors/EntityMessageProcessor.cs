using System;
using System.IO;
using FlyingTuna.Additions.Serialization;
using FlyingTuna.Entities;
using FlyingTuna.Factories;
using FlyingTuna.MPI;

namespace FlyingTuna.Networking.Processors
{
    public class EntityMessageProcessor : IPacketProcessor
    {
        private readonly MsgDes _msgd;
        private readonly IEntityCollection _collection;

        public EntityMessageProcessor(IHost host, IEntityCollection collection)
        {
            _msgd = host.ServiceManager.GetProvider<MsgDes>();
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


            // We need to deserialize the message
            var msg = _msgd.GetMsg(messageId, reader);

            Console.WriteLine("Received {0} message from {1}.", msg.GetType(), c);

            // Send the message
            ent.SendMessageAs(c, (Message) msg);
        }
    }
}
