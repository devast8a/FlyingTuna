using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FlyingTuna.MPI;

namespace FlyingTuna.Additions.Serialization
{
    public class MsgDes
    {
        public List<Type> Types = new List<Type>();
        private readonly BinaryFormatter _serializer;

        public MsgDes(LinqExpSerializer serializer)
        {
            _serializer = new BinaryFormatter();
        }

        public void Add(params Type[] types)
        {
            Types.AddRange(types);
        }

        public Message GetMsg(int id, BinaryReader reader)
        {
            return (Message)_serializer.Deserialize(reader.BaseStream);
        }

        public int GetMsgId(Message message)
        {
            var index = Types.IndexOf(message.GetType());

            if (index == -1)
            {
                throw new Exception("Not registered");
            }

            return index;
        }

        public void Serialize(Message message, BinaryWriter writer)
        {
            //GetMsgId(message);
            //_serializer.GetSerializer(message.GetType())(writer, message);
            
            _serializer.Serialize(writer.BaseStream, message);
        }
    }
}
