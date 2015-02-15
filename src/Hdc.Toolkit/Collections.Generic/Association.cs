namespace Hdc.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class Association<TKey, TValue>
    {
        public Association(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public Association(KeyValuePair<TKey, TValue> keyValuePair)
        {
            Key = keyValuePair.Key;
            Value = keyValuePair.Value;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public KeyValuePair<TKey, TValue> ToKeyValuePair()
        {
            return new KeyValuePair<TKey, TValue>(Key, Value);
        }
    }
}