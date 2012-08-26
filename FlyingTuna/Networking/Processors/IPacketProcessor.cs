namespace FlyingTuna.Networking.Processors
{
    public interface IPacketProcessor
    {
        void Process(Connection c, byte[] buffer, short pos, short len);
    }

    public class EmptyPacketProcessor : IPacketProcessor
    {
        public void Process(Connection c, byte[] buffer, short pos, short len)
        {
            throw new System.NotImplementedException();
        }
    }
}