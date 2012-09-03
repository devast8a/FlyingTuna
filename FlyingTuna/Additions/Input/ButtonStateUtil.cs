namespace FlyingTuna.Additions.Input
{
    public class ButtonStateUtil
    {
        public static ButtonState GetState(ButtonState previous, bool pressed)
        {
            if (pressed)
            {
                return previous == ButtonState.Up ? ButtonState.Pressed : ButtonState.Down;
            }

            return previous == ButtonState.Down ? ButtonState.Released : ButtonState.Up;
        }
    }
}
