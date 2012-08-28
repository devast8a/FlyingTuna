using System.IO;
using FlyingTuna.MPI;

namespace FlyingTuna.Networking.Packets
{
    public class EntityMessage : IPacket
    {
        public EntityMessage(IHost host, int entity, Message message)
        {
            _serializer = host.ServiceManager.GetProvider<MsgDes>();
            Entity = entity;
            Message = message;
        }

        public readonly Message Message;
        public readonly int Entity;
        private readonly MsgDes _serializer;

        public byte Number
        {
            get { return (byte) PacketNumbers.EntitySendMessage; }
        }

        public short WriteToStream(BinaryWriter writer)
        {
            writer.Write(Entity);
            writer.Write(_serializer.GetMsgId(Message));

            var pos = writer.BaseStream.Position;

            _serializer.Serialize(Message, writer);

            return (short) (8 + writer.BaseStream.Position - pos);
        }
    }
}