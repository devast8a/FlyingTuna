﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FlyingTuna.MPI;

namespace FlyingTuna
{
    public class MsgDes
    {
        public List<Type> Types = new List<Type>();
        private readonly LinqExpSerializer _serializer;

        public MsgDes(LinqExpSerializer serializer)
        {
            _serializer = serializer;
        }

        public void Add(params Type[] types)
        {
            foreach (var type in types)
            {
                Console.WriteLine("Generating serializer for: " + type);
                _serializer.GetSerializer(type);
                Types.Add(type);
            }
        }

        public Message GetMsg(int id, BinaryReader reader)
        {
            return (Message)_serializer.GetDeserializer(Types[id])(reader);
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
            GetMsgId(message);
            _serializer.GetSerializer(message.GetType())(writer, message);
        }
    }
}
