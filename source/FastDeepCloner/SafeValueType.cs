using System.Collections.Concurrent;

namespace FastDeepCloner
{
    /// <summary>
    /// CustomValueType Dictionary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="P"></typeparam>
    public class SafeValueType<T, P> : ConcurrentDictionary<T, P>
    {
        public SafeValueType(ConcurrentDictionary<T, P> dictionary = null)
        {
            if (dictionary == null)
            {
                return;
            }

            foreach (var item in dictionary)
            {
                TryAdd(item.Key, item.Value);
            }
        }

        public bool TryAdd(T key, P item, bool overwrite = false)
        {
            if (ContainsKey(key) && !overwrite)
            {
                return true;
            }

            if (overwrite && ContainsKey(key))
            {
                TryRemove(key, out _);
            }

            base.TryAdd(key, item);


            return true;
        }

        public P GetOrAdd(T key, P item, bool overwrite = false)
        {
            if (ContainsKey(key) && !overwrite)
            {
                return base[key];
            }

            if (overwrite && ContainsKey(key))
            {
                TryRemove(key, out _);
            }

            base.TryAdd(key, item);
            return base[key];
        }

        public P Get(T key)
        {
            if (ContainsKey(key))
            {
                return this[key];
            }

            object o = null;
            return (P)o;
        }
    }
}
