using System;

namespace FlyingTuna.MPI
{
    public class Message
    {
        public Message()
        {
            if(GetType().Name == "DrawMessage")
            {
                return;
            }

            Console.WriteLine("Message created: " + GetType().Name);
        }

        public ID Identifier;
    }
}