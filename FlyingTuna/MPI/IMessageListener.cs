using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingTuna.MPI
{
    public interface IMessageListener
    {
        void SendMessage(Message message);
    }
}
