using System;
using FlyingTuna.Additions.IdenSys;

namespace FlyingTuna.MPI
{
    public class Message
    {
        [NonSerialized]
        public ID Identifier;

        public bool NoExtraLogging;
    }
}