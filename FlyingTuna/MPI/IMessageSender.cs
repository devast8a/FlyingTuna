using System;
using System.Linq;
using System.Text;

namespace FlyingTuna.MPI
{
    public interface IMessageSender : IMessageListener, IHasMetadata
    {
        void Error<T, TError>(T message, TError error)
            where T : Message
            where TError : ErrorMessage;
    }
}
