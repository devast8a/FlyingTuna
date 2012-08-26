using System.IO;
using FlyingTuna.Entities;

namespace FlyingTuna.Networking.Packets
{
    public class EntityNew : IPacket
    {
        public EntityNew(EntityType type, int id, EntityNewFlags flags)
        {
            Type=type;
            Flags=(byte) flags;
            ID = id;
        }

        public EntityType Type;
        public byte Flags;
        public int ID;

        public byte Number
        {
            get { return (byte) PacketNumbers.EntityNew; }
        }

        public short WriteToStream(BinaryWriter writer)
        {
            writer.Write(Type.Identifier.IdentifierNumber);
            writer.Write(ID);
            writer.Write(Flags);

            return 9;
        }
    }

    public enum EntityNewFlags : byte
    {
        None = 0,
        UseCustomConstructor = 1, // Constructor 0 = Send
    }
}
