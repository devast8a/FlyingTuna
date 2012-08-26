using System.IO;

namespace FlyingTuna.Networking.Packets
{
    public interface IPacket
    {
        byte Number { get; }
        short WriteToStream(BinaryWriter writer);
    }
}
