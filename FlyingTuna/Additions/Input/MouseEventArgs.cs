using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyingTuna.MPI;

namespace FlyingTuna.Additions.Input
{
    public class MouseEventArgs : Message
    {
        public MouseEventArgs()
        {
            NoExtraLogging = true;
        }

        public GameMouseState MouseState;
    }
}
