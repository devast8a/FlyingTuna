using System.IO;
using FlyingTuna.MPI;

namespace FlyingTuna.Networking.Packets
{
    public class EntityMessage : IPacket
    {
        public EntityMessage(int entity, Message message)
        {
            Entity = entity;
            Message = message;
        }

        public readonly Message Message;
        public readonly int Entity;

        public byte Number
        {
            get { return (byte) PacketNumbers.EntitySendMessage; }
        }

        public short WriteToStream(BinaryWriter writer)
        {
            writer.Write(Entity);
            writer.Write(Message.Identifier.IdentifierNumber);

            return 8;
        }
    }
}