using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingTuna.MPI
{
    public interface IMessageSender : IMessageListener
    {
        void Error<T, TError>(T message, TError error)
            where T : Message
            where TError : ErrorMessage;
    }
}
