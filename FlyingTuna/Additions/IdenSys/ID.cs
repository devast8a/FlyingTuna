namespace FlyingTuna.Additions.IdenSys
{
    public class ID
    {
        public string Name;

        // Temporary name
        public int IdentifierNumber = -1;

        public static implicit operator ID(int identifier)
        {
            return new ID {IdentifierNumber = identifier};
        }
    }
}
