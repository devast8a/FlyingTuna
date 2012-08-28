using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingTuna
{
    public class ID
    {
        public string Name;

        // Temporary name
        public int IdentifierNumber;

        public static implicit operator ID(int identifier)
        {
            return new ID {IdentifierNumber = identifier};
        }
    }
}
