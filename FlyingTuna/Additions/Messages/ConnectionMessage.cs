using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using FlyingTuna.MPI;
using FlyingTuna.Networking;

namespace FlyingTuna.Additions.Messages
{
    public abstract class ConnectionMessage : Message
    {
        public Connection Connection{get; private set;}

        protected ConnectionMessage(Connection connection)
        {
            Connection = connection;
        }
    }
}
