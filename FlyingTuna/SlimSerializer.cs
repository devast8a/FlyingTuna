using System;
using System.Collections.Generic;
using System.IO;

namespace FlyingTuna
{
    public static class SlimSerializer
    {
        public const byte EndOfStream = 0;

        public const byte Byte = 1;
        public const byte Short = 2;
        public const byte Int = 3;
        public const byte Float = 4;
        public const byte Double = 5;
        public const byte String = 6;
        public const byte Entity = 7;
        public const byte BooleanTrue = 8;
        public const byte BooleanFalse = 9;
        public const byte Array = 10;

        public static object[] Deserialize(BinaryReader reader)
        {
            byte type;
            var obj = new List<object>();

            while ((type = reader.ReadByte()) != EndOfStream)
            {
                switch (type)
                {
                    case BooleanFalse:
                        obj.Add(false);
                        break;

                    case BooleanTrue:
                        obj.Add(true);
                        break;

                    case Byte:
                        obj.Add(reader.ReadByte());
                        break;

                    case Short:
                        obj.Add(reader.ReadInt16());
                        break;

                    case Int:
                        obj.Add(reader.ReadInt32());
                        break;

                    case Array:
                        obj.Add(Deserialize(reader));
                        break;

                    default:
                        throw new Exception("Unable to deserialize type " + type);
                }
            }

            return obj.ToArray();
        }

        public static void Serialize(BinaryWriter writer, params object[] objs)
        {
            foreach(var v in objs)
            {
                SerializeObject(writer, v);
            }

            writer.Write(EndOfStream);
        }

        public static void SerializeObject(BinaryWriter writer, object obj)
        {
            var type = obj.GetType();
            
            if(type == typeof(object[]))
            {
                writer.Write(Array);

                foreach(var o in (object[])obj)
                {
                    Serialize(writer, o);
                }

                writer.Write(EndOfStream);
                return;
            }
            
            if(type == typeof(int))
            {
                writer.Write(Int);
                writer.Write((int)obj);
                return;
            }
            
            if(type == typeof(bool))
            {
                writer.Write((bool)obj ? BooleanTrue : BooleanFalse);
                return;
            }

            throw new Exception("Unable to serialize type");
        }
    }
}
