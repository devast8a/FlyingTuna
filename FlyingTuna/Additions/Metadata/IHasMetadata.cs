using System.Collections.Generic;

namespace FlyingTuna.Additions.Metadata
{
    public interface IHasMetadata
    {
        Dictionary<string, object> GetMetadata();
    }
}