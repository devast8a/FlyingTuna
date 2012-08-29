using System.Collections.Generic;

namespace FlyingTuna.Additions.Metadata
{
    public class MetadataContainer : IHasMetadata
    {
        protected Dictionary<string, object> Metadata = new Dictionary<string, object>();
        public Dictionary<string, object> GetMetadata()
        {
            return Metadata;
        }
    }
}
