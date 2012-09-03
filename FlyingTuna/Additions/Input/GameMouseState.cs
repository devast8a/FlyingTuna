using System;
using Microsoft.Xna.Framework;

namespace FlyingTuna.Additions.Input
{
    public class GameMouseState
    {
        /// <summary>
        /// Position of mouse.
        /// Top-left of window is 0, 0
        /// </summary>
        public Point Position;

        /// <summary>
        /// Scroll Wheel Value
        /// </summary>
        public int ScrollWheelValue;

        /// <summary>
        /// Contains state of all mouse buttons
        /// </summary>
        private readonly ButtonState[] _buttonStates = new ButtonState[5];

        #region Buttons as properties
        /// <summary>
        /// Current state of Left mouse Button
        /// </summary>
        public ButtonState Left
        {
            get { return _buttonStates[SKeys.MouseLeft - SKeys.MouseOffset]; }
        }
        /// <summary>
        /// Current state of Middle mouse Button
        /// </summary>
        public ButtonState Middle
        {
            get { return _buttonStates[SKeys.MouseMiddle - SKeys.MouseOffset]; }
        }
        /// <summary>
        /// Current state of Right mouse button
        /// </summary>
        public ButtonState Right
        {
            get { return _buttonStates[SKeys.MouseRight - SKeys.MouseOffset]; }
        }

        /// <summary>
        /// Current state of the extra mouse button 1
        /// </summary>
        public ButtonState XButton1
        {
            get { return _buttonStates[SKeys.MouseX1 - SKeys.MouseOffset]; }
        }

        /// <summary>
        /// Current state of the extra mouse button 2
        /// </summary>
        public ButtonState XButton2
        {
            get { return _buttonStates[SKeys.MouseX2 - SKeys.MouseOffset]; }
        }
        #endregion Buttons as properties

        #region Special Key Handling
        public ButtonState Get(SKeys keys)
        {
            if ((keys & SKeys.MouseFlag) != SKeys.MouseFlag)
            {
                throw new ArgumentException("Invalid SKeys type");
            }

            return _buttonStates[keys - SKeys.MouseOffset];
        }

        public bool IsDown(SKeys key)
        {
            return Get(key) == ButtonState.Down;
        }

        public bool IsUp(SKeys key)
        {
            return Get(key) == ButtonState.Up;
        }

        public bool IsPressed(SKeys key)
        {
            return Get(key) == ButtonState.Pressed;
        }

        public bool IsReleased(SKeys key)
        {
            return Get(key) == ButtonState.Released;
        }
        #endregion Special Key Handling

        public void UpdateState(int x, int y, bool[] states, int scrollwheel)
        {
            if(states.Length != 5)
            {
                throw new ArgumentException("Must have a length of 5", "states");
            }

            // Update mouse position
            Position.X = x;
            Position.Y = y;

            // Update button states
            for(int i = 0 ; i < 5 ; i++)
            {
                _buttonStates[i] = ButtonStateUtil.GetState(_buttonStates[i], states[i]);
            }

            //TODO: Work out a better way of providing this date to the developer (if possible)
            ScrollWheelValue = scrollwheel;
        }
    }
}