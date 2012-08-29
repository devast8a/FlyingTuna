using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingTuna.Utils.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            TValue value;
            return dict.TryGetValue(key, out value) ? value : default(TValue);
        }
    }
}
