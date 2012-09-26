using System;
using FlyingTuna.Additions.IdenSys;

namespace FlyingTuna.MPI
{
    [Serializable]
    public class Message
    {
        [NonSerialized]
        public ID Identifier;

        [NonSerialized]
        public bool NoExtraLogging;
    }
}