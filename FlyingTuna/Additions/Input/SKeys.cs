namespace FlyingTuna.Additions.Input
{
    /// <summary>
    /// Special Keys to be used along side the Keys enum for handling input
    /// </summary>
    public enum SKeys : byte
    {
        LayoutVersion  = 1,

        // Toggle-able Keys
        ToggleKeysFlag      = 0x80,
        ToggleKeysOffset    = 0x81,
        CapsLock    = 0x81,
        NumLock     = 0x82,
        ScrollLock  = 0x83,

        // Left/Right independent Modifier Keys
        ModifiersFlag   = 0x40,
        ModifiersOffset = 0x41,
        Shift       = 0x41,
        Ctrl        = 0x42,
        Alt         = 0x43,
        
        // Mouse Buttons
        MouseFlag   = 0x20,
        MouseOffset = 0x21,
        MouseLeft   = 0x21,
        MouseMiddle = 0x22,
        MouseRight  = 0x23,
        MouseX1     = 0x24,
        MouseX2     = 0x25,

        // TODO: Provide joystick button handling
    }
}
