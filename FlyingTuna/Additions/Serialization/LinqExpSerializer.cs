using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using FlyingTuna.Utils.Extensions.ICustomAttributeProvider;

namespace FlyingTuna.Additions.Serialization
{
    public class LinqExpSerializer
    {
        readonly Dictionary<Type, string> _types; 


        public Dictionary<Type, Func<BinaryReader, object>> Deserializers = new Dictionary<Type, Func<BinaryReader, object>>();
        public Dictionary<Type, Action<BinaryWriter, object>> Serializers = new Dictionary<Type, Action<BinaryWriter, object>>();

        public LinqExpSerializer()
        {
            _types = new Dictionary<Type, string>();

            _types.Add(typeof(int), "ReadInt32");
            _types.Add(typeof(byte), "ReadByte");
            _types.Add(typeof(string), "ReadString");
        }

        public byte[] Serialize(object obj)
        {
            var m = new MemoryStream();
            var w = new BinaryWriter(m);

            GetSerializer(obj.GetType())(w, obj);

            return m.GetBuffer();
        }

        public T Deserialize<T>(byte[] bytes)
        {
            var m = new MemoryStream(bytes);
            var r = new BinaryReader(m);

            return (T)GetDeserializer(typeof(T))(r);
        }
        
        public Action<BinaryWriter, object> GetSerializer(Type type)
        {
            Action<BinaryWriter, object> value;
            if(Serializers.TryGetValue(type, out value))
            {
                return value;
            }

            CreateSerializers(type);

            if (Serializers.TryGetValue(type, out value))
            {
                return value;
            }

            throw new Exception();
        }

        public Func<BinaryReader, object> GetDeserializer(Type type)
        {
            Func<BinaryReader, object> value;
            if (Deserializers.TryGetValue(type, out value))
            {
                return value;
            }

            CreateSerializers(type);

            if (Deserializers.TryGetValue(type, out value))
            {
                return value;
            }

            throw new Exception();
        }

        private string GetSerializerForType(Type type)
        {
            string value;
            return _types.TryGetValue(type, out value) ? value : null;
        }

        private void CreateSerializers(Type type)
        {
            var options = type.GetCustomAttributeOrNull<SerializeOptionsAttribute>(true);
            
            Expression expression;

            if(options != null)
            {
                
            }

            var constructor = type.GetConstructor(new Type[]{});

            if(constructor == null)
            {
                throw new Exception();
            }

            // Create the object to begin with
            var dExp = new List<Expression>();
            var dStr = Expression.Parameter(typeof(BinaryReader), "reader");
            var dRet = Expression.Variable(type, "ret");

            var returnTarget = Expression.Label(typeof(object));
            
            var sExp = new List<Expression>();
            var sStr = Expression.Parameter(typeof(BinaryWriter), "writer");
            var sDataParam = Expression.Parameter(typeof(object), "o1");
            var sData = Expression.Variable(type, "o2");
            
            dExp.Add(Expression.Assign(dRet, Expression.New(constructor)));
            sExp.Add(Expression.Assign(sData, Expression.Convert(sDataParam, type)));

            foreach (var field in type.GetFields())
            {
                var fieldOption = field.GetCustomAttributeOrNull<NonSerializedAttribute>();
                var name = GetSerializerForType(field.FieldType);

                if(name == null || fieldOption != null)
                {
                    continue;
                }


                dExp.Add(Expression.Assign(Expression.Field(dRet, field), Expression.Call(dStr, name, new Type[0])));
                sExp.Add(Expression.Call(sStr, "Write", null, Expression.Field(sData, field)));
            }

            dExp.Add(Expression.Label(returnTarget, Expression.Convert(dRet, typeof(object))));

            var block = Expression.Block(typeof(object), new[]{dRet}, dExp);
            var des = Expression.Lambda<Func<BinaryReader, object>>(block, dStr).Compile();

            Serializers.Add(type, Expression.Lambda<Action<BinaryWriter, object>>(Expression.Block(new[]{sData}, sExp), sStr, sDataParam).Compile());
            Deserializers.Add(type, des);
        }
    }
}