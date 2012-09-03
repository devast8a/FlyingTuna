using System;
using Microsoft.Xna.Framework.Input;

namespace FlyingTuna.Additions.Input.Backend
{
    public static class XnaMouse
    {
        private static readonly bool[] States = new bool[5];

        public static void Update(int n, GameMouseState state)
        {
            if(n != 0)
            {
                throw new Exception();
            }

            var s = Mouse.GetState();

            States[0] = s.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            States[1] = s.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            States[2] = s.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            States[3] = s.XButton1 == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            States[4] = s.XButton2 == Microsoft.Xna.Framework.Input.ButtonState.Pressed;

            state.UpdateState(
                s.X,
                s.Y,
                States,
                s.ScrollWheelValue
            );
        }
    }
}