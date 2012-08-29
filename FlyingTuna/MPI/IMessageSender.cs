using System;
using System.Linq;
using System.Text;
using FlyingTuna.Additions;
using FlyingTuna.Additions.Metadata;

namespace FlyingTuna.MPI
{
    public interface IMessageSender : IMessageListener, IHasMetadata
    {
        void Error(Message message, ErrorMessage error);
    }
}
