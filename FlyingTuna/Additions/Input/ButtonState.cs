namespace FlyingTuna.Additions.Input
{
    public enum ButtonState : byte
    {
        // This key is not valid
        NotValid = 0x00,

        // The key is currently up
        Up       = 0x01,

        // The key is currently down
        Down     = 0x02,

        // The key has been released ie. It was down the frame before and now it's up
        Released = NewEvent | Up,

        // The key has been pressed ie. It was up the frame before and now it's down
        Pressed  = NewEvent | Down,

        // Mark for Released/Pressed
        NewEvent = 0x04,
    }
}