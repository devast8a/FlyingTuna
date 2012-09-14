using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingTuna.MPI
{
    /// <summary>
    /// Defines a class capable of sending messages to all attached listeners
    /// </summary>
    public interface IMessageSource
    {
        void AddListener(IMessageListener listener);
        void RemoveListener(IMessageListener listener);
        IEnumerable<IMessageListener> GetListeners(); 
    }
}
