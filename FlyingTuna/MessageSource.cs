using System.Collections.Generic;
using FlyingTuna.MPI;

namespace FlyingTuna
{
    public class MessageSource : IMessageSource
    {
        private readonly List<IMessageListener> _listeners = new List<IMessageListener>(); 

        public void AddListener(IMessageListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(IMessageListener listener)
        {
            _listeners.Remove(listener);
        }

        public IEnumerable<IMessageListener> GetListeners()
        {
            return _listeners;
        }

        public void SendMessage(Message message)
        {
            foreach(var listener in _listeners)
            {
                listener.SendMessage(message);
            }
        }
    }
}