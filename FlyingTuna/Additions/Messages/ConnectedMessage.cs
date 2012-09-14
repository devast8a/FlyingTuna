using FlyingTuna.Networking;

namespace FlyingTuna.Additions.Messages
{
    public class ConnectedMessage : ConnectionMessage
    {
        public ConnectedMessage(Connection connection) : base(connection)
        {
        }
    }
}