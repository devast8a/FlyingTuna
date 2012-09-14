using FlyingTuna.Networking;

namespace FlyingTuna.Additions.Messages
{
    public class DisconnectedMessage : ConnectionMessage
    {
        public DisconnectedMessage(Connection connection)
            : base(connection)
        {
        }
    }
}