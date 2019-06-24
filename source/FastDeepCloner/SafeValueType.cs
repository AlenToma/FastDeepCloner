using System.Collections.Generic;

namespace FastDeepCloner
{
    /// <summary>
    /// CustomValueType Dictionary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="P"></typeparam>
    public class SafeValueType<T, P> : Dictionary<T, P>
    {
        public SafeValueType(Dictionary<T, P> dic = null)
        {
            if (dic != null)
                foreach (var item in dic)
                    TryAdd(item.Key, item.Value);
        }

        public bool TryAdd(T key, P item, bool overwrite = false)
        {
            if (base.ContainsKey(key) && !overwrite)
                return true;

            if (overwrite && this.ContainsKey(key))
                this.Remove(key);
            base.Add(key, item);


            return true;
        }

        public P GetOrAdd(T key, P item, bool overwrite = false)
        {
            if (base.ContainsKey(key) && !overwrite)
                return base[key];

            if (overwrite && this.ContainsKey(key))
                this.Remove(key);
            base.Add(key, item);
            return base[key];
        }

        public P Get(T key)
        {
            if (this.ContainsKey(key))
                return this[key];
            object o = null;
            return (P)o;
        }
    }
}
