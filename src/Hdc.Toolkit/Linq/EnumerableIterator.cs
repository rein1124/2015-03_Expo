using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    internal class EnumerableIterator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public EnumerableIterator(IEnumerable<T> enumerable)
        {
            _enumerator = enumerable.GetEnumerator();
            MoveNext();
        }

        public bool HasCurrent { get; private set; }

        public T Current
        {
            get { return _enumerator.Current; }
        }

        public void MoveNext()
        {
            HasCurrent = _enumerator.MoveNext();
        }
    }
}