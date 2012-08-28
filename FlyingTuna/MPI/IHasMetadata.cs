using System.Collections.Generic;

namespace FlyingTuna.MPI
{
    public interface IHasMetadata
    {
        Dictionary<string, object> GetMetadata();
    }
}