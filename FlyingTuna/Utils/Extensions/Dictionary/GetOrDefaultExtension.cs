using System.Collections.Generic;

namespace FlyingTuna.Utils.Extensions.Dictionary
{
    public static class GetOrDefaultExtension
    {
        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            TValue value;
            return dict.TryGetValue(key, out value) ? value : default(TValue);
        }

        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            TValue value;
            return dict.TryGetValue(key, out value) ? value : defaultValue;
        }
    }
}
