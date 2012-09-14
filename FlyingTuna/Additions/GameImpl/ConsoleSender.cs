using System;
using System.Collections.Generic;
using FlyingTuna.MPI;

namespace FlyingTuna.Additions.GameImpl
{
    public class ConsoleSender : IMessageSender
    {
        public void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        private readonly Dictionary<string, object> _data = new Dictionary<string, object>(); 
        public Dictionary<string, object> GetMetadata()
        {
            return _data;
        }

        public void Error(Message message, ErrorMessage error)
        {
            Console.WriteLine(error.ToString());
            throw new Exception(error.ToString());
        }
    }
}
